using System;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText {
    public class Alpha : BeginScope {
        public Alpha(int alpha) : base(RichTextScopeType.Alpha, true, $"#{Convert.ToString(alpha, 16)}") { }

        public Alpha(float alpha) : base(RichTextScopeType.Alpha, true, $"#{Convert.ToString((int)(alpha * 100), 16)}") { }
    }
}
