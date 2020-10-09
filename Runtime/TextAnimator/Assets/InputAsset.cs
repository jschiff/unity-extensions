using Input = Com.Jschiff.UnityExtensions.TextAnimator.Content.Input;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Assets {
    
    public class InputAsset : AnimatedItemAsset<Input>
    {
        [SerializeField]
        bool PrintInput;
        [SerializeField]
        int MaximumCharacters;

        protected override Input GetItem() {
            return new Input(Tag, PrintInput, MaximumCharacters);
        }
    }
    
}
