using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator{

    [Serializable]
    public class TextAnimatorContext {
        const float defaultRevealRate = 20f;

        public delegate void TextChangedHandler(TextAnimatorContext source, bool visible, AnimatedItem item);
        public event TextChangedHandler OnTextChanged;

        public delegate void CursorEnabledHandler(TextAnimatorContext source, bool enabled);
        public event CursorEnabledHandler OnCursorEnabled;

        public TextContent Content { get; internal set; }

        Dictionary<string, int> _markedPositions = new Dictionary<string, int>();
        List<AnimatedItem> _items = new List<AnimatedItem>();
        Stack<RichTextScopeType> _richTextScopes = new Stack<RichTextScopeType>();
        internal TextOptions options = TextOptions.Default;
        internal Queue<AnimatedItem> ToRender = new Queue<AnimatedItem>();
        internal StringBuilder rawTextBuilder = new StringBuilder();
        readonly Dictionary<string, string> input = new Dictionary<string, string>();
        public float CursorOn { get; private set; } = .5f;
        public float CursorOff { get; private set; } = .5f;

        public int CursorPosition { get; set; } = 0;

        public int CursorPositionRelativeToEnd {
            set {
                CursorPosition = value;
            }
        }

        bool _cursorEnabled = false;
        public bool CursorEnabled {
            get => _cursorEnabled;
            private set {
                var oldValue = _cursorEnabled;
                _cursorEnabled = value;
                if (oldValue != value) {
                    OnCursorEnabled?.Invoke(this, value);
                }
            }
        }

        public ReadOnlyCollection<RichTextScopeType> Scopes =>
            Array.AsReadOnly(_richTextScopes.AsEnumerable().ToArray());
        public int ActiveScopeCount => _richTextScopes.Count;

        public void ClearAllRichTextScopes(AnimatedItem source) {
            while (_richTextScopes.Count > 0) {
                PopScope(source);
            }
        }

        public void SetCursorBlinkRate(float on, float off) {
            CursorOn = on;
            CursorOff = off;
        }

        public void EnableCursor(bool enabled) {
            CursorEnabled = enabled;
        }

        /**
         * To be called from the Animate method of a content item. Animates content according to the current CharacterRate
         */
        public IEnumerator AnimateText(string content, TextOptions options, AnimatedItem source) {
            float timeElapsed = 0;

            options = this.options.OverrideWith(options);
            float revealRate = options.RevealRate;
            if (revealRate <= 0) {
                revealRate = this.options.RevealRate <= 0 ? defaultRevealRate : this.options.RevealRate;
            }

            float timeBetweenElements = 1 / revealRate;
            int elementsDisplayed = 0;
            if (options.Element == TextOptions.TextElement.None) {
                options.Element = TextOptions.TextElement.Character;
            }

            switch (options.Element) {
                case TextOptions.TextElement.Character:
                    var elements = content;
                    while (elementsDisplayed < elements.Length) {
                        AppendRawInstantly(elements[elementsDisplayed], true, source);
                        elementsDisplayed += 1;

                        // Wait <timeBetweenChars> seconds after each character, including the last one.
                        // Don't use WaitForSeconds because we want to save partial time between frames.
                        while (timeElapsed < timeBetweenElements) {
                            yield return null;
                            timeElapsed += Time.deltaTime;
                        }
                        timeElapsed -= timeBetweenElements;
                    }
                    break;
                case TextOptions.TextElement.Word:
                    if (options.SplitOnDelimeters.Length == 0) {
                        options.SplitOnDelimeters = TextOptions.defaultDelimeters;
                    }
                    string[] strings = SplitAndKeepDelimeters(content, options.SplitOnDelimeters);
                    while (elementsDisplayed < strings.Length) {
                        var toAdd = strings[elementsDisplayed++];
                        if (elementsDisplayed < strings.Length) {
                            toAdd += strings[elementsDisplayed++];
                        }

                        AppendRawInstantly(toAdd, true, source);
                        //Debug.Log($"Adding {toAdd}");
                        //Debug.Log(Time.time);

                        // Wait <timeBetweenChars> seconds after each character, including the last one.
                        // Don't use WaitForSeconds because we want to save partial time between frames.
                        while (timeElapsed < timeBetweenElements) {
                            yield return null;
                            timeElapsed += Time.deltaTime;
                        }
                        timeElapsed -= timeBetweenElements;
                    }
                    break;
                default:
                    throw new Exception($"Unexpected element type {options.Element}");
            }
        }

        private string[] SplitAndKeepDelimeters(string toSplit, string[] delimeters) {
            Regex regex = new Regex(string.Join("|", delimeters.Select(d => $"({d})")));
            return regex.Split(toSplit);
        }

        public ReadOnlyDictionary<string, int> MarkedPositions { get; private set; }
        public ReadOnlyCollection<AnimatedItem> RenderedItems { get; private set; }

        // If the context is dirty, the animator must redraw the entire context.
        public bool Dirty { get; internal set; }
        public float RevealRate {
            get => options.RevealRate;
            set { options.RevealRate = value; }
        }

        public void MarkDirty() {
            Dirty = true;
            ToRender.Clear();
            _markedPositions.Clear();
        }



        public void UnmarkPosition(string marker) {
            _markedPositions.Remove(marker);
        }

        public TextAnimatorContext() {
            MarkedPositions = new ReadOnlyDictionary<string, int>(_markedPositions);
            RenderedItems = new ReadOnlyCollection<AnimatedItem>(_items);
        }

        public void PushScope(RichTextScopeType scopeType, AnimatedItem source, params string[] args) {
            PushScope(scopeType, source, false, args);
        }

        public void PushScope(RichTextScopeType scopeType, AnimatedItem source, bool useRawTag = false, params string[] args) {
            _richTextScopes.Push(scopeType);
            AppendRawInstantly(scopeType.GetOpenTag(useRawTag, args), false, source);
        }

        public RichTextScopeType PopScope(AnimatedItem source) {
            var poppedType = _richTextScopes.Pop();
            AppendRawInstantly(poppedType.GetCloseTag(), false, source);
            return poppedType;
        }

        public void MarkCurrentPosition(string marker) {
            if (_markedPositions.ContainsKey(marker)) {
                throw new Exception($"Context already contained marker {marker}. Markers must be unique");
            }
            _markedPositions[marker] = _items.Count;
        }

        internal void ProcessItem(AnimatedItem item) {
            item.OnEnqueued(this);
            _items.Add(item);

            ToRender.Enqueue(item);
        }

        public void AppendRawInstantly(string s, bool isVisible, AnimatedItem source) {
            rawTextBuilder.Append(s);
            OnTextChanged?.Invoke(this, isVisible, source);
        }

        public void AppendRawInstantly(char c, bool isVisible, AnimatedItem source) {
            rawTextBuilder.Append(c);
            OnTextChanged?.Invoke(this, isVisible, source);
        }

        /// <summary>
        /// DANGER DANGER
        /// </summary>
        /// <param name="n">The number of characters to remove</param>
        public string RemoveRawInstantly(int n, AnimatedItem source) {
            char[] removed = new char[n];
            rawTextBuilder.CopyTo(rawTextBuilder.Length - n - 1, removed, 0, n);
            rawTextBuilder.Length -= n;
            OnTextChanged?.Invoke(this, true, source);

            return new string(removed);
        }

        public string EvaluateText() {
            StringBuilder sb = new StringBuilder();
            sb.Append(rawTextBuilder.ToString());

            // Close all open scopes.
            var clonedStack = new Stack<RichTextScopeType>(_richTextScopes.Reverse());
            while(clonedStack.Count > 0) {
                var scope = clonedStack.Pop();
                sb.Append(scope.GetCloseTag());
            }

            return sb.ToString();
        }

        public string GetLastNCharacters(int n) {
            string currString = rawTextBuilder.ToString();
            return currString.Substring(currString.Length - n);
        }

        public void SetInput(string key, string data) {
            input[key] = data;
        }

        public string GetInputValue(string key) {
            if (input.TryGetValue(key, out string value)) {
                return value;
            }

            return "";
        }
    }
}
