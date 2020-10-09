namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText {
    public class Align : BeginScope {
        public enum Alignment {
            Left,
            Center,
            Right
        }

        private static string AsString(Alignment a) {
            switch (a) {
                case Alignment.Left:
                    return "\"left\"";
                case Alignment.Center:
                    return "\"center\"";
                case Alignment.Right:
                    return "\"right\"";
            }

            return "";
        }

        public Align(Alignment alignment) : base(RichTextScopeType.Align, AsString(alignment)) {

        }
    }
}
