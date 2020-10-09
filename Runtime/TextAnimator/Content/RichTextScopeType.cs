using System.Collections.Generic;
using System.Text;

namespace Com.Jschiff.UnityExtensions.TextAnimator{
    public enum RichTextScopeType {
        Align,
        Alpha,
        Bold,
        CharacterSpacing,
        Color,
        Font,
        Indentation,
        Italic,
        LineHeight,
        Mark,
        NonBreaking,
        NoParse,
        Underline,
        Size,
        StrikeThrough,
        Style,
        SubScript,
        Superscript,
        Width
    }

    public static class RichTextScopeExtensions {
        private const string OpenBracket = "<";
        private const string CloseBracket = ">";
        private const string CloseTagFormat = OpenBracket + "/{0}" + CloseBracket;

        // Curse C# enums not being like Java enums!
        private static readonly Dictionary<RichTextScopeType, string> tagTokens = new Dictionary<RichTextScopeType, string>() {
            {RichTextScopeType.Align, "align" },
            {RichTextScopeType.Alpha, "alpha" },
            {RichTextScopeType.Bold, "b" },
            {RichTextScopeType.CharacterSpacing, "cspace" },
            {RichTextScopeType.Color, "color" },
            {RichTextScopeType.Font, "font" },
            {RichTextScopeType.Indentation, "indent" },
            {RichTextScopeType.Italic, "i" },
            {RichTextScopeType.LineHeight, "line-height" },
            {RichTextScopeType.Mark, "mark" },
            {RichTextScopeType.NonBreaking, "nobr" },
            {RichTextScopeType.NoParse, "noparse" },
            {RichTextScopeType.Size, "size" },
            {RichTextScopeType.StrikeThrough, "s" },
            {RichTextScopeType.Style, "style" },
            {RichTextScopeType.SubScript, "sub" },
            {RichTextScopeType.Superscript, "sup" },
            {RichTextScopeType.Underline, "u" },
            {RichTextScopeType.Width, "width" },
        };

        private static readonly HashSet<RichTextScopeType> hasParameter = new HashSet<RichTextScopeType>() {
            RichTextScopeType.Align,
            RichTextScopeType.Alpha,
            RichTextScopeType.CharacterSpacing,
            RichTextScopeType.Color,
            RichTextScopeType.Font,
            RichTextScopeType.Indentation,
            RichTextScopeType.LineHeight,
            RichTextScopeType.Mark,
            RichTextScopeType.Size,
            RichTextScopeType.Style,
            RichTextScopeType.Width
        };

        private static string BuildOpenTagFormatString(this RichTextScopeType scopeType) {
            StringBuilder sb = new StringBuilder();
            sb.Append(OpenBracket);
            sb.Append(tagTokens[scopeType]);
            if (hasParameter.Contains(scopeType)) {
                sb.Append("={0}");
            }
            sb.Append(">");

            return sb.ToString();
        }

        public static string GetOpenTag(this RichTextScopeType scopeType, bool useRawTag, params string[] args) {
            string fmtString = scopeType.BuildOpenTagFormatString();
            int expectedParamSize = hasParameter.Contains(scopeType) ? 1 : 0;
            if (args.Length != expectedParamSize) {
                throw new System.Exception($"Expected {expectedParamSize} arguments for scope type {scopeType} but got {args.Length}.");
            }

            if (useRawTag) {
                return $"{OpenBracket}{args[0]}{CloseBracket}";
            }

            var openTag = string.Format(fmtString, args);
            return string.Format(openTag, args);
        }

        public static string GetCloseTag(this RichTextScopeType scopeType) {
            return string.Format(CloseTagFormat, tagTokens[scopeType]);
        }
    }
}