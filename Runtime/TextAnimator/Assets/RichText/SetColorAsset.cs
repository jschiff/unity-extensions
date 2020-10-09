using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText{
    public class SetColorAsset : AnimatedItemAsset<SetColor> {
        [SerializeField]
        Color color;

        protected override SetColor GetItem() {
            return new SetColor(color);
        }
    }
}
