using Com.Jschiff.UnityExtensions.TextAnimator.Content;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    
    public class SetCharacterRateAsset : AnimatedItemAsset<SetCharacterRate>
    {
        [SerializeField]
        float CharacterRate;

        protected override SetCharacterRate GetItem() {
            return new SetCharacterRate(CharacterRate);
        }
    }
    
}
