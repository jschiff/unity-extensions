using UnityEngine;

namespace Com.Jschiff.UnityExtensions {
    public static class TransformExtensions {
        public static Vector3 Left(this Transform t) => -t.right;
        public static Vector3 Down(this Transform t) => -t.up;
        public static Vector3 Back(this Transform t) => -t.forward;
    }
}
