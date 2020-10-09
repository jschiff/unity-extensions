namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText{
    public class Bold : BeginScope {
        public static readonly Bold Instance = new Bold();

        Bold() : base(RichTextScopeType.Bold) { }
    }
}
