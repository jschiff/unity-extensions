using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator{
    public class HelloWorldTerminalContent : TextContent {

        public IEnumerator<AnimatedItem> GetEnumerator() {
            yield return ResetRichTextAttributes.Instance;

            yield return new SetCharacterRate(5.0f);
            yield return new WaitForTime(1.0f);
            yield return new BasicText("Hello");
            yield return new SetCharacterRate(1.0f);
            yield return new BasicText("...");
            yield return new SetColor(Color.blue);
            yield return new WaitForTime(1f);
            yield return NewLine.Instance;

            yield return new SetCharacterRate(10f);
            yield return new BasicText("World!");

            yield return new SetCharacterRate(1f);
            yield return new BasicText("Hey there chicken little tell me your name. I've got a dinner plate here emblazoned with your name.", TextOptions.Words());
            yield return NewLine.Instance;
            yield return new SubContent(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
