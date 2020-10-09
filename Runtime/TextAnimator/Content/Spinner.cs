using System;
using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Com.Jschiff.UnityExtensions.TextAnimator.Content {

    [Serializable]
    public class Spinner : AnimatedItem {
        public readonly ReadOnlyCollection<string> Content;
        public readonly float ChangeInterval;
        public readonly float Duration = 5f;
        public readonly string FinalContent;

        public Spinner(float changeInterval, float duration, string finalContent, params string[] content) {
            this.Content = Array.AsReadOnly(content);
            this.ChangeInterval = changeInterval;
            this.Duration = duration;
            this.FinalContent = finalContent;
        }

        public override IEnumerator Animate(TextAnimatorContext context) {
            float totalTime = Duration;
            float changeTime = 0;
            int contentIndex = -1;
            string lastShown = "";

            while (totalTime > 0) {
                totalTime -= Time.deltaTime;
                changeTime += Time.deltaTime;

                if (changeTime > ChangeInterval) {
                    changeTime -= ChangeInterval;
                    contentIndex = (contentIndex + 1) % Content.Count;
                    string toShow = Content[contentIndex];

                    context.RemoveRawInstantly(lastShown.Length, this);
                    context.AppendRawInstantly(toShow, true, this);
                    lastShown = toShow;
                }

                yield return null;
            }

            if (FinalContent != null) {
                context.RemoveRawInstantly(lastShown.Length, this);
                context.AppendRawInstantly(FinalContent, true, this);
            }
        }
    }
}
