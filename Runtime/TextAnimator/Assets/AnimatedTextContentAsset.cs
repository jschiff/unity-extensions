using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {

    [CreateAssetMenu(fileName = "New Animated Text Content", menuName = "Text Animator/Animated Text Content")]
    public class AnimatedTextContentAsset : ScriptableObject, TextContent {
        public TextOptions Options = TextOptions.Default;
        public List<AnimatedItemAsset> Items = new List<AnimatedItemAsset>();

        public IEnumerator<AnimatedItem> GetEnumerator() {
            foreach (var asset in Items) {
                yield return asset.Item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
