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
        public static Vector3 WithZ(this Vector2 v, float z) {
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

        public static Vector3 Clamp(this Vector3 v, Vector3 min, Vector3 max) {
            v.x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x;
            v.y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y;
            v.z = v.z < min.z ? min.z : v.z > max.z ? max.z : v.z;
            return v;
        }

        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) {
            v.x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x;
            v.y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y;
            return v;
        }

        public static Vector3 ScreenYInvert(this Vector3 vector) {
            return new Vector3(vector.x, Screen.height - vector.y, 0);
        }

        public static Color WithA(this Color color, float a) {
            return new Color(color.r, color.g, color.b, a);
        }
    }
}
