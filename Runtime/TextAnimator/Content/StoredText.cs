using System.Collections;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class StoredText : AnimatedItem {
        public readonly string Tag = "";
        public readonly string Default;
        public readonly TextOptions Options = default;

        public StoredText(string tag, string @default, TextOptions options) {
            Tag = tag;
            Default = @default;
            Options = options;
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            if (Options.Instant) {
                context.PushScope(RichTextScopeType.NoParse, this);
                context.AppendRawInstantly(context.GetInputValue(Tag), true, this);
                context.PopScope(this);
            }
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
            if (!Options.Instant) {
                context.PushScope(RichTextScopeType.NoParse, this);
                yield return context.AnimateText(context.GetInputValue(Tag), Options, this);
                context.PopScope(this);
            }
        }
    }
}
