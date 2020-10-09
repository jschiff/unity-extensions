namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText{
    public class BeginScope : AnimatedItem {
        public readonly RichTextScopeType ScopeType;
        public readonly string[] Args;
        public readonly bool useRawTag = false;

        public BeginScope(RichTextScopeType scopeType, params string[] args) : this(scopeType, false, args) { }

        public BeginScope(RichTextScopeType scopeType, bool useRawTag, params string[] args) {
            ScopeType = scopeType;
            this.useRawTag = useRawTag;
            Args = args;
        }

        public override void AnimateInstantly(TextAnimatorContext context) {
            context.PushScope(ScopeType, this, useRawTag, Args);
        }
    }
}
