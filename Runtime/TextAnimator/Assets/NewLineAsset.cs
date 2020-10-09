using Com.Jschiff.UnityExtensions.TextAnimator.Content;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    public class NewLineAsset : AnimatedItemAsset<NewLine>
    {
        protected override NewLine GetItem() {
            return NewLine.Instance;
        }
    }
    
}
