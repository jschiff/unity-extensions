namespace Com.Jschiff.UnityExtensions.TextAnimator.Content {
    
    public class EnableCursor : AnimatedItem
    {
        public readonly bool Enable;

        public static readonly EnableCursor EnabledInstance = new EnableCursor(true);
        public static readonly EnableCursor DisabledInstance = new EnableCursor(false);

        EnableCursor(bool enable) {
            Enable = enable;
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            context.EnableCursor(Enable);
        }
    }
    
}
