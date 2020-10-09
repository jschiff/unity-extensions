using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public class WaitForTimeAsset : AnimatedItemAsset<WaitForTime> {
        [SerializeField]
        float TimeInSeconds = 1f;
        [SerializeField]
        bool Unscaled = false;

        protected override WaitForTime GetItem() {
            return new WaitForTime(TimeInSeconds, Unscaled);
        }
    }

}
