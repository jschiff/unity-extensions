namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText{
    // TODO add material support
    public class SetFont : BeginScope {
        public SetFont(string font) : base(RichTextScopeType.Font, $"\"{font}\"") { }
    }
}
