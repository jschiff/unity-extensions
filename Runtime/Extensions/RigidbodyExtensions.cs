using UnityEngine;

namespace Com.Jschiff.UnityExtensions {
    public static class RigidbodyExtensions {
        public static Vector3 Forward(this Rigidbody rb) => rb.rotation * Vector3.forward;
        public static Vector3 Left(this Rigidbody rb) => rb.rotation * Vector3.left;
        public static Vector3 Right(this Rigidbody rb) => rb.rotation * Vector3.right;
        public static Vector3 Up(this Rigidbody rb) => rb.rotation * Vector3.up;
        public static Vector3 Down(this Rigidbody rb) => rb.rotation * Vector3.down;
        public static Vector3 Back(this Rigidbody rb) => rb.rotation * Vector3.back;

        public static void VisibleAddForce(this Rigidbody rb, Vector3 force, ForceMode mode = ForceMode.Force) {
            switch (mode) {
                case ForceMode.Force:
                    rb.velocity += (force * Time.fixedDeltaTime) / rb.mass;
                    break;
                case ForceMode.Acceleration:
                    rb.velocity += force * Time.fixedDeltaTime;
                    break;
                case ForceMode.Impulse:
                    rb.velocity += force / rb.mass;
                    break;
                case ForceMode.VelocityChange:
                    rb.velocity += force;
                    break;
                default:
                    throw new System.Exception($"Unknown force mode " + mode);
            }
        }

        public static void VisibleAddRelativeForce(this Rigidbody rb, Vector3 force, ForceMode mode = ForceMode.Force) {
            force = rb.rotation * force;

            switch (mode) {
                case ForceMode.Force:
                    rb.velocity += (force * Time.fixedDeltaTime) / rb.mass;
                    break;
                case ForceMode.Acceleration:
                    rb.velocity += force * Time.fixedDeltaTime;
                    break;
                case ForceMode.Impulse:
                    rb.velocity += force / rb.mass;
                    break;
                case ForceMode.VelocityChange:
                    rb.velocity += force;
                    break;
                default:
                    throw new System.Exception($"Unknown force mode " + mode);
            }
        }
    }
}
