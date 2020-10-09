using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText {
    class FontAsset : AnimatedItemAsset<SetFont> {
        [SerializeField]
        string Font;

        protected override SetFont GetItem() {
            return new SetFont(Font);
        }
    }
}
