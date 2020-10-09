using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    
    public class UnsafeTextAsset : AnimatedItemAsset<UnsafeText>
    {
        [SerializeField]
        string RawText;

        protected override UnsafeText GetItem() {
            return new UnsafeText(RawText);
        }
    }
    
}
