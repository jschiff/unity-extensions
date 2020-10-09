namespace Com.Jschiff.UnityExtensions.TextAnimator.Content{
    public class NewLine : UnsafeText {
        public static readonly NewLine Instance = new NewLine();

        NewLine() : base('\n') { }
    }
}
