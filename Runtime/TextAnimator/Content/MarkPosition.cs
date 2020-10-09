namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class MarkPosition : AnimatedItem {
        public readonly string marker;

        public MarkPosition(string marker) {
            this.marker = marker;
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            context.MarkCurrentPosition(marker);
        }
    }
}
