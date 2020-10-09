using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    public abstract class AnimatedItemAsset<T> : AnimatedItemAsset where T : AnimatedItem {
        public override AnimatedItem Item { 
            get {
                var item = GetItem();
                item.Tag = Tag;
                return GetItem();
            }
        }

        protected abstract T GetItem();


        public override string ListPreview => "";

        public override string ListLabel => typeof(T).Name;

        public override int ScopeDepthChange {
            get {
                return typeof(BeginScope).IsAssignableFrom(typeof(T)) ? 1 :
                    typeof(EndScope).IsAssignableFrom(typeof(T)) ? -1 : 0;
            }
        }
    }
}
