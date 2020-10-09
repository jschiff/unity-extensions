using UnityEngine;
using Space = Com.Jschiff.UnityExtensions.TextAnimator.Content.Space;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public class SpaceAsset : AnimatedItemAsset<Space> {
        [SerializeField]
        float Space;

        protected override Space GetItem() {
            return new Space(Space);
        }
    }

}
