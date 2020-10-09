namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class SetCharacterRate : AnimatedItem {
        public readonly float rate;

        public SetCharacterRate(float rate) {
            this.rate = rate;
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            context.RevealRate = rate;
        }
    }
}
