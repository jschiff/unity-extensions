using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public class StoredTextAsset : AnimatedItemAsset<StoredText> {
        [SerializeField]
        string Tag;
        [SerializeField]
        string Default;
        [SerializeField]
        TextOptions Options;

        protected override StoredText GetItem() {
            return new StoredText(Tag, Default, Options);
        }
    }

}
