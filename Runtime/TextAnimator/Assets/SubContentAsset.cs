using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public class SubContentAsset : AnimatedItemAsset<SubContent> {
        [SerializeField]
        public AnimatedTextContentAsset SubContent;

        protected override SubContent GetItem() {
            return new SubContent(SubContent);
        }
    }

}
