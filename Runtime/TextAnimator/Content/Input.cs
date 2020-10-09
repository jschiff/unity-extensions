using System;
using System.Collections;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content {
    [SerializeField]
    public class Input : AnimatedItem {
        const char NewLine = '\n';
        const char CarriageReturn = '\r';
        const char BackSpace = '\b';

        public readonly bool PrintInput = true;
        public readonly int MaximumCharacters = 100;

        Keyboard keyboard;

        public Input(string tag, bool printInput, int maximumCharacters) : base(tag) {
            PrintInput = printInput;
            MaximumCharacters = maximumCharacters;
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
#if ENABLE_INPUT_SYSTEM
            bool exit = false;
            int charactersAdded = 0;

            context.PushScope(RichTextScopeType.NoParse, this);

            Action<char> keyboardInputCallback = (char c) => {
                // In case we get a callback after we have stopped accepting input.
                if (exit) return;

                //Debug.Log($"Input received: \"{c}\"");

                if (PrintInput && IsPrintable(c) && charactersAdded < MaximumCharacters) {
                    context.AppendRawInstantly(c, true, this);
                    charactersAdded++;
                }
                else if (IsNewLine(c)) {
                    exit = true;
                    context.SetInput(Tag, context.GetLastNCharacters(charactersAdded));
                }
                else if (c == BackSpace && charactersAdded > 0) {
                    context.RemoveRawInstantly(1, this);
                    charactersAdded--;
                }

                context.CursorPositionRelativeToEnd = 0;
            };

            keyboard = Keyboard.current;
            keyboard.onTextInput += keyboardInputCallback;
            context.EnableCursor(true);
            while (!exit) {
                yield return null;
            }
            context.EnableCursor(false);
            keyboard.onTextInput -= keyboardInputCallback;
            context.PopScope(this);
#else
            throw new NotImplementedException("Input item is only supported for unity's new Input System.");
#endif
        }

        private static bool IsPrintable(char c) {
            return !IsNewLine(c) && (
                char.IsLetterOrDigit(c) ||
                char.IsWhiteSpace(c) && !IsNewLine(c) ||
                char.IsPunctuation(c) ||
                char.IsSymbol(c));
        }

        private static bool IsNewLine(char c) {
            return c == NewLine || c == CarriageReturn;
        }
    }
}
