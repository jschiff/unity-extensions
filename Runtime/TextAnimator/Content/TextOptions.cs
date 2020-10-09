using System;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content {
    [Serializable]
    public class TextOptions {
        public static TextOptions Default => Characters();

        /// <summary>
        /// The rate at which text elements are revealed
        /// </summary>
        public float RevealRate = 5f;
        public bool Instant = false;

        /// <summary>
        /// Element types
        /// </summary>
        public TextElement Element = TextElement.None;

        //public readonly SplitMode SplitBy;
        public string[] SplitOnDelimeters = defaultDelimeters;

        public static readonly string[] defaultDelimeters = new string[] { "\\s" };

        public enum TextElement {
            None = 0,
            Character = 1,
            Word = 2,
        }

        TextOptions(bool instant, float revealRate = -1, TextElement element = TextElement.None, string[] splitOn = null) {
            Instant = instant;
            RevealRate = revealRate;
            Element = element;
            SplitOnDelimeters = splitOn;
        }

        public static TextOptions Instantaneous() {
            return new TextOptions(instant: true, revealRate: -1, element: TextElement.None);
        }

        public static TextOptions Characters(float revealRate = -1) {
            return new TextOptions(instant: false, revealRate: revealRate, element: TextElement.Character);
        }

        public static TextOptions Words(string[] splitOn = null, float revealRate = -1) {
            if (splitOn == null) splitOn = defaultDelimeters;

            return new TextOptions(instant: false, revealRate: revealRate, element: TextElement.Word, splitOn: splitOn);
        }

        internal TextOptions OverrideWith(TextOptions overrider) {
            if (overrider.RevealRate <= 0) {
                overrider.RevealRate = this.RevealRate;
            }

            if (overrider.SplitOnDelimeters == null) {
                overrider.SplitOnDelimeters = this.SplitOnDelimeters;
            }

            if (overrider.Element == TextElement.None) {
                overrider.Element = this.Element;
            }

            return overrider;
        }
    }
}
