using Com.Jschiff.UnityExtensions.TextAnimator.Content;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    public class ResetRichTextAttributesAsset : AnimatedItemAsset<ResetRichTextAttributes>
    {
        protected override ResetRichTextAttributes GetItem() {
            return ResetRichTextAttributes.Instance;
        }
    }
}
