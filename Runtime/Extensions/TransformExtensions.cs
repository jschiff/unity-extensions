using UnityEngine;

namespace Com.Jschiff.UnityExtensions {
    public static class TransformExtensions {
        public static Vector3 Left(this Transform t) => -t.right;
        public static Vector3 Down(this Transform t) => -t.up;
        public static Vector3 Back(this Transform t) => -t.forward;

        public static void PointWorldVectorAt(this Transform t, Vector3 worldVec, Transform target) {
            PointWorldVectorAt(t, worldVec, target.position);
        }
        public static void PointWorldVectorAt(this Transform t, Vector3 worldVec, Vector3 target) {
            Vector3 dir = target - t.position;
            Quaternion diff = Quaternion.FromToRotation(worldVec, dir);

            t.rotation *= diff;
        }

        public static void PointLocalVectorAt(this Transform t, Vector3 localVec, Transform target) {
            Vector3 worldVec = t.TransformVector(localVec);
            PointWorldVectorAt(t, worldVec, target.position);
        }

        public static void PointLocalVectorAt(this Transform t, Vector3 localVec, Vector3 target) {
            Vector3 worldVec = t.TransformVector(localVec);
            PointWorldVectorAt(t, worldVec, target);
        }
    }
}
