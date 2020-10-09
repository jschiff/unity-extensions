namespace Com.Jschiff.UnityExtensions.TextAnimator.Content {

    /// <summary>
    /// Instantly appends raw text. No guarding against rich text tags is performed.
    /// </summary>
    public class UnsafeText : AnimatedItem {
        readonly string rawText;

        public UnsafeText(string rawText) {
            this.rawText = rawText;
        }

        public UnsafeText(char rawText) {
            this.rawText = rawText.ToString();
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            context.AppendRawInstantly(rawText, true, this);
        }
    }
}
