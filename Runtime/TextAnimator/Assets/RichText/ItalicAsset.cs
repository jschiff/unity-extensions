using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText{
    public class ItalicAsset : AnimatedItemAsset<Italic> {
        protected override Italic GetItem() {
            return Italic.Instance;
        }
    }
}
