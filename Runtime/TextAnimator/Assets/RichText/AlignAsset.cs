using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets.RichText{
    public class AlignAsset : AnimatedItemAsset<Align> {

        [SerializeField]
        Align.Alignment Alignment;

        protected override Align GetItem() {
            return new Align(Alignment);
        }
    }
}
