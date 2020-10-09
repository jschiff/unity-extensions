using Com.Jschiff.UnityExtensions.TextAnimator.Assets;
using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator {
    public class TextAnimator : MonoBehaviour {
        [SerializeField]
        TMP_Text target;
        [SerializeField]
        TextAnimatorContext context;
        [SerializeField]
        AnimatedTextContentAsset contentAsset;
        [SerializeField]
        Texture2D cursorTexture;

        public delegate void TextChangedHandler(TextAnimator source, bool visible, AnimatedItem item);
        public event TextChangedHandler OnTextChanged;

        public TextContent Content { get; set; }
        public TextOptions Options;

        Coroutine animationCoroutine = null;

        bool addCursor = false;
        float timeSinceCursorShown = 0;

        private void Awake() {
            if (contentAsset != null) {
                Content = contentAsset;
                Options = contentAsset.Options;
            }
        }

        public void Play() {
            StartAnimating(Content, Options);
        }

        public void StartAnimating(TextContent content) {
            StartAnimating(content, TextOptions.Default);
        }

        public void StartAnimating(TextContent content, TextOptions textOptions) {
            if (animationCoroutine != null) {
                Debug.LogWarning("Tried to start animation, but it is already playing");
                return;
            }

            animationCoroutine = StartCoroutine(Animate(content, textOptions));
        }

        private IEnumerator Animate(TextContent content, TextOptions textOptions) {
            TextAnimatorContext context = new TextAnimatorContext();
            context.options = textOptions;

            this.context = context;
            context.OnTextChanged += WhenContextTextChanges;
            context.OnCursorEnabled += WhenCursorEnabled;
            foreach (var currentItem in content) {
                context.ProcessItem(currentItem);

                yield return StartCoroutine(Render(context));
            }

            context.OnTextChanged -= WhenContextTextChanges;
            context.OnCursorEnabled -= WhenCursorEnabled;
            animationCoroutine = null;
            yield break;
        }

        private IEnumerator Render(TextAnimatorContext context) {
            while (context.ToRender.Count > 0) {
                var nextItem = context.ToRender.Dequeue();

                nextItem.AnimateInstantly(context);
                yield return StartCoroutine(nextItem.Animate(context));
            }
        }

        private void WhenCursorEnabled(TextAnimatorContext context, bool enabled) {
            // Show cursor immediately when it is enabled
            ResetCursorBlink();
            Redraw(context);
        }

        private void WhenContextTextChanges(TextAnimatorContext context, bool visible, AnimatedItem source) {
            // Show cursor immediately when typing a character
            ResetCursorBlink();
            Redraw(context);
            OnTextChanged?.Invoke(this, visible, source);
        }

        void ResetCursorBlink() {
            timeSinceCursorShown = 0;
            addCursor = true;
        }

        void Redraw(TextAnimatorContext context) {
            var evaluatedText = context.EvaluateText();
            if (addCursor && context.CursorEnabled) {
                evaluatedText += cursor;
            }

            target.text = evaluatedText;
        }

        private void Update() {
            if (context == null) {
                return;
            }

            HandleCursorBlink();
        }

        const string cursor = "<mark=#FFFFFFFF>m</mark>";

        private void HandleCursorBlink() {
            if (!context.CursorEnabled) return;

            timeSinceCursorShown += Time.deltaTime;

            float toCheck = addCursor ? context.CursorOn : context.CursorOff;
            if (timeSinceCursorShown > toCheck) {
                timeSinceCursorShown -= toCheck;

                addCursor = !addCursor;
                Redraw(context);
            }

            //var textInfo = target.textInfo;
            //var characterCount = textInfo.characterCount;
            //var targetTransform = target.transform;
            ////Debug.Log($"There are {target.textInfo.characterCount} characters");
            //var charInfo = target.textInfo.characterInfo[characterCount];



            //Debug.DrawLine(targetTransform.TransformPoint(charInfo.topLeft), targetTransform.TransformPoint(charInfo.bottomRight), Color.green);
        }
    }
    
}
