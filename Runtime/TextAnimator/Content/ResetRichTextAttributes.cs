using System.Collections;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class ResetRichTextAttributes : AnimatedItem {
        public static readonly ResetRichTextAttributes Instance = new ResetRichTextAttributes();

        private ResetRichTextAttributes() { }

        public override IEnumerator Animate(TextAnimatorContext context) {
            context.ClearAllRichTextScopes(this);
            yield break;
        }
    }
}
