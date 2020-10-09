using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public class SpinnerAsset : AnimatedItemAsset<Spinner> {
        [SerializeField]
        string[] Content;
        [SerializeField]
        float ChangeInterval;
        [SerializeField]
        float Duration = 5f;
        [SerializeField]
        string FinalContent;

        protected override Spinner GetItem() {
            return new Spinner(ChangeInterval, Duration, FinalContent, Content);
        }
    }

}
