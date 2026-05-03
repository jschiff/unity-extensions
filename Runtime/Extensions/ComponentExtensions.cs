using UnityEngine;

namespace Com.Jschiff.UnityExtensions {
    public static class ComponentExtensions {
        public static bool TryGetComponentInParent<T>(this Component c, out T component) {
            component = c.GetComponentInParent<T>();
            return component != null;
        }

        // GetComponentInParent(bool includeInactive) wasn't added until Unity 2020.1, so we walk manually.
        public static bool TryGetComponentInParent<T>(this Component c, bool includeInactive, out T component) {
            Transform current = c.transform;
            while (current != null) {
                if (includeInactive || current.gameObject.activeInHierarchy) {
                    if (current.TryGetComponent(out component)) {
                        return true;
                    }
                }
                current = current.parent;
            }
            component = default;
            return false;
        }

        public static bool TryGetComponentInParent<T>(this GameObject go, out T component) {
            component = go.GetComponentInParent<T>();
            return component != null;
        }

        public static bool TryGetComponentInParent<T>(this GameObject go, bool includeInactive, out T component) {
            return go.transform.TryGetComponentInParent(includeInactive, out component);
        }

        public static bool TryGetComponentInChildren<T>(this Component c, out T component) {
            component = c.GetComponentInChildren<T>();
            return component != null;
        }

        public static bool TryGetComponentInChildren<T>(this Component c, bool includeInactive, out T component) {
            component = c.GetComponentInChildren<T>(includeInactive);
            return component != null;
        }

        public static bool TryGetComponentInChildren<T>(this GameObject go, out T component) {
            component = go.GetComponentInChildren<T>();
            return component != null;
        }

        public static bool TryGetComponentInChildren<T>(this GameObject go, bool includeInactive, out T component) {
            component = go.GetComponentInChildren<T>(includeInactive);
            return component != null;
        }
    }
}
