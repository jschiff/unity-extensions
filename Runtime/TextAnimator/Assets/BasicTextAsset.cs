using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    public class BasicTextAsset : AnimatedItemAsset<BasicText> {
        [SerializeField]
        [TextArea]
        string Content;

        [SerializeField]
        TextOptions Options;

        protected override BasicText GetItem() {
            return new BasicText(Content, Options);
        }

        public override string ListPreview => $"{base.ListPreview} {Content}";
    }
}
