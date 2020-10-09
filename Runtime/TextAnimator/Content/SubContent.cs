using System.Collections;
using System.Collections.Generic;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{

    public class SubContent : AnimatedItem {
        readonly IEnumerable<AnimatedItem> content;

        public SubContent(IEnumerable<AnimatedItem> content) {
            this.content = content;
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
            foreach (var item in content) {
                context.ProcessItem(item);
            }

            yield break;
        }
    }
}
