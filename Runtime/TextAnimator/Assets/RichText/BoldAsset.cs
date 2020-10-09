using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText{
    public class BoldAsset : AnimatedItemAsset<Bold> {
        protected override Bold GetItem() {
            return Bold.Instance;
        }
    }
}
