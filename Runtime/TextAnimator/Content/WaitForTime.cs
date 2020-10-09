using System.Collections;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class WaitForTime : AnimatedItem {
        public readonly float seconds;
        public readonly bool unscaled;

        object yieldInstruction;

        public WaitForTime(float seconds, bool unscaled = false) {
            this.seconds = seconds;
            this.unscaled = unscaled;

            yieldInstruction = unscaled ?
                (object) new WaitForSecondsRealtime(seconds) :
                new WaitForSeconds(seconds);
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
            yield return yieldInstruction;
        }
    }
}
