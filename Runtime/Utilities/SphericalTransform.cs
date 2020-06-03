using UnityEngine;

namespace Com.Jschiff.UnityExtensions.Utilities {
    [ExecuteAlways]
    public class SphericalTransform : MonoBehaviour {
        [SerializeField]
        [Tooltip("Elevation angle from the x/z reference plane, in degrees")]
        public float θ;
        public float Theta {
            get => θ;
            set {
                θ = value;
                UpdatePosition();
            }
        }
        public float ThetaRads {
            get => Theta * Mathf.Deg2Rad;
            set {
                Theta = value * Mathf.Rad2Deg;
            }
        }

        [SerializeField]
        [Tooltip("Azimuth angle, in degrees")]
        public float φ;
        public float Phi {
            get => φ;
            set {
                φ = value;
                UpdatePosition();
            }
        }
        public float PhiRads {
            get => Phi * Mathf.Deg2Rad;
            set {
                Phi = value * Mathf.Rad2Deg;
            }
        }

        [SerializeField]
        [Tooltip("Radius of sphere")]
        public float r = 50f;
        public float Radius {
            get => r;
            set {
                r = value;
                UpdatePosition();
            }
        }

        public Vector3 position {
            get => new Vector3(Theta, Phi, Radius);
            set {
                θ = value.x;
                φ = value.y;
                r = value.z;
                UpdatePosition();
            }
        }

        public Vector3 positionRads {
            get => new Vector3(ThetaRads, PhiRads, Radius);
            set {
                θ = value.x * Mathf.Rad2Deg;
                φ = value.y * Mathf.Rad2Deg;
                r = value.z;
                UpdatePosition();
            }
        }

        public Vector3 PostRoll = Vector3.zero;

#if UNITY_EDITOR
        // Allow live updates on backing fields in inspector only
        private void LateUpdate() {
            UpdatePosition();
        }
#endif

        private void Start() {
            UpdatePosition();
        }

        void UpdatePosition() {
            // Update position:
            Vector3 localPosition = SphericalCoordinateHelper.SphericalToEuclidean(θ * Mathf.Deg2Rad, φ * Mathf.Deg2Rad, r);
            transform.localPosition = localPosition;

            SphericalCoordinateHelper.RotateProperly(transform, transform.localPosition);
            transform.Rotate(PostRoll, Space.Self);
        }
    }
}
