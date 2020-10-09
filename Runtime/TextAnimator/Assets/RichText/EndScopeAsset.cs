using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText {
    public class EndScopeAsset : AnimatedItemAsset<EndScope> {
        readonly EndScope endScope = new EndScope();

        protected override EndScope GetItem() {
            return endScope;
        }
    }
}
