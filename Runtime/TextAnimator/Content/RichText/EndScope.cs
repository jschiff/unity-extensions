using System.Collections;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText {
    public class EndScope : AnimatedItem {
        public static readonly EndScope Instance = new EndScope();

        public override IEnumerator Animate(TextAnimatorContext context) {
            var poppedScope = context.PopScope(this);
            context.AppendRawInstantly(poppedScope.GetCloseTag(), false, this);

            yield break;
        }
    }
}
