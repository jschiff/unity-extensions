namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText{
    public class Italic : BeginScope {
        public static readonly Italic Instance = new Italic();

        Italic() : base(RichTextScopeType.Italic) { }
    }
}
