using System;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content.RichText{
    public class SetColor : BeginScope {
        public SetColor(Color c) : base(RichTextScopeType.Color, true, $"#{GetRGBHexString(c)}") { }
        public SetColor(Color32 c) : base(RichTextScopeType.Color, true, $"#{GetRGBHexString(c)}") { }

        private static string GetRGBHexString(Color32 c) {
            string r = Convert.ToString(c.r, 16);
            string g = Convert.ToString(c.g, 16);
            string b = Convert.ToString(c.b, 16);
            string a = Convert.ToString(c.a, 16);

            return $"{r}{g}{b}{a}";
        }
    }
}
