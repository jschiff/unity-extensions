using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using System;
using System.Collections;

namespace Com.Jschiff.UnityExtensions.TextAnimator {

    /// <summary>
    /// Add text to the animator over time according to the current context settings.
    /// This guards against rich text tags in the content.
    /// 
    /// Optionally, the text can be appended instantly.
    /// </summary>
    [Serializable]
    public class BasicText : AnimatedItem {
        public readonly string Content = "";
        public readonly TextOptions Options = default;

        public BasicText(string content, TextOptions options) {
            this.Content = content;
            this.Options = options;
        }

        public BasicText(string content) : this(content, TextOptions.Characters()) { }

        public override void AnimateInstantly(TextAnimatorContext context) {
            if (Options.Instant) {
                context.PushScope(RichTextScopeType.NoParse, this);
                context.AppendRawInstantly(Content, true, this);
                context.PopScope(this);
            }
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
            if (!Options.Instant) {
                context.PushScope(RichTextScopeType.NoParse, this);
                yield return context.AnimateText(Content, Options, this);
                context.PopScope(this);
            }
        }
    }
}
