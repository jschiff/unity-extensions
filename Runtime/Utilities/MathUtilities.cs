using UnityEngine;

public static class MathUtilities {
    public static float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh) {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
    
    public static float Remap(float input, float oldHigh, float newHigh) {
        return Remap(0, oldHigh, 0, newHigh, input);
    }

    public static float RoundToNearest(float input, float toNearest) {
        float scaled = input / toNearest;
        float rounded = Mathf.Round(scaled);
        return rounded * toNearest;
    }
}
