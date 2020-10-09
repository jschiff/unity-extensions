using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    
    public class EnableCursorAsset : AnimatedItemAsset<EnableCursor>
    {
        [SerializeField]
        bool Enabled;

        protected override EnableCursor GetItem() {
            return Enabled ? EnableCursor.EnabledInstance : EnableCursor.DisabledInstance;
        }
    }
    
}
