using System;
using System.Collections;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{

    [Serializable]
    public abstract class AnimatedItem {
        public string Tag;

        protected AnimatedItem() {
            Tag = null;
        }

        protected AnimatedItem(string tag) {
            Tag = tag;
        }

        public virtual void OnEnqueued(TextAnimatorContext context) {

        }

        public virtual IEnumerator Animate(TextAnimatorContext context) {
            yield break;
        }

        public virtual void AnimateInstantly(TextAnimatorContext context) {

        }
    }
}
