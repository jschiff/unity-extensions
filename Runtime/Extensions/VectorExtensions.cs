using UnityEngine;

namespace Com.Jschiff.UnityExtensions {
    public static class VectorExtensions {
        public static Vector2 WithX(this Vector2 v, float x) {
            v.x = x;
            return v;
        }
        public static Vector2 WithY(this Vector2 v, float y) {
            v.y = y;
            return v;
        }
        public static Vector2 WithZ(this Vector2 v, float z) {
            Vector3 v3 = v;
            v3.z = z;
            return v3;
        }

        public static Vector3 WithX(this Vector3 v, float x) {
            v.x = x;
            return v;
        }
        public static Vector3 WithY(this Vector3 v, float y) {
            v.y = y;
            return v;
        }
        public static Vector3 WithZ(this Vector3 v, float z) {
            v.z = z;
            return v;
        }
        
        public static Vector3 Hadamard(this Vector3 a, Vector3 b) {
            return new Vector3(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z
            );
        }
        
        public static Vector2 Hadamard(this Vector2 a, Vector2 b) {
            return new Vector2(
                a.x * b.x,
                a.y * b.y
            );
        }

        public static Vector3 ScreenYInvert(this Vector3 vector) {
            return new Vector3(vector.x, Screen.height - vector.y, 0);
        }

        public static Color WithA(this Color color, float a) {
            return new Color(color.r, color.g, color.b, a);
        }
    }
}
