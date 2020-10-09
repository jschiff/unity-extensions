using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    public abstract class AnimatedItemAsset : ScriptableObject {
        [SerializeField]
        public string Tag;

        public abstract AnimatedItem Item { get; }
        public abstract string ListPreview { get; }
        public abstract string ListLabel { get; }

        public virtual int ScopeDepthChange => 0;
    }
}
