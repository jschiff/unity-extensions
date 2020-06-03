using UnityEngine;

namespace Com.Jschiff.UnityExtensions.Utilities {
    // We reuse Vector3 for polar coordinates, with x = θ, y = φ, z = r
    public static class SphericalCoordinateHelper {
        public const float SPHERE_RADIUS = 50f;
        public const float TWO_PI = Mathf.PI * 2;
        public static Vector3 SPHERE_ORIGIN = Vector3.zero;

        public static Vector3 SphericalToEuclidean(Vector2 sphericalCoords) {
            return SphericalToEuclidean(sphericalCoords.WithZ(SPHERE_RADIUS));
        }

        public static Vector3 SphericalToEuclidean(Vector3 coords) {
            return SphericalToEuclidean(coords.x, coords.y, coords.z);
        }

        public static Vector3 SphericalToEuclidean(float θ, float φ, float r = SPHERE_RADIUS) {
            φ *= -1;

            var sinTheta = Mathf.Sin(θ);
            var cosTheta = Mathf.Cos(θ);
            var sinPhi = Mathf.Sin(φ);
            var cosPhi = Mathf.Cos(φ);

            var y = r * sinTheta;
            var z = r * cosTheta * sinPhi;
            var x = r * cosTheta * cosPhi;

            // z / x = sinPhi/cosPhi
            // z / x = tan(Phi)
            // arctan(z / x) = phi

            return new Vector3(x, y, z);
        }

        public static Vector3 SphericalLerpWithEuclideanInputs(Vector3 startEuclidean, Vector3 endEuclidean, float t) {
            Vector3 start = EuclideanToSpherical(startEuclidean);
            Vector3 end = EuclideanToSpherical(endEuclidean);

            Vector3 lerped = ClockwiseWrappedSphericalLerp(start, end, t);
            Vector3 backToEuclidean = SphericalToEuclidean(lerped);
            return backToEuclidean;
        }

        public static Vector3 SphericalLerp(Vector3 startSphericalDegrees, Vector3 endSphericalDegrees, float t) {
            Vector3 startRads = DegreesToRads(startSphericalDegrees);
            Vector3 endRads = DegreesToRads(endSphericalDegrees);
            Vector3 lerped = ClockwiseWrappedSphericalLerp(startSphericalDegrees, endSphericalDegrees, t);
            Vector3 inRads = DegreesToRads(lerped);
            Vector3 euclidean = SphericalToEuclidean(inRads);
            return euclidean;
        }

        public static void RotateProperly(Transform transform, Vector3 outFromCenterOfSphere) {
            Vector3 normal = outFromCenterOfSphere;
            Vector3 normalProjection = Vector3.ProjectOnPlane(normal, Vector3.up);
            Vector3 shouldBeForward = -Vector3.Cross(normalProjection, Vector3.up);

            transform.localRotation = Quaternion.LookRotation(shouldBeForward, -normal);
        }

        public static Vector3 AntiClockwiseWrappedSphericalLerp(Vector3 start, Vector3 end, float t) {
            while (end.y > start.y) {
                end.y = end.y - TWO_PI;
            }
            return Vector3.Lerp(start, end, t);
        }

        public static Vector3 ClockwiseWrappedSphericalLerp(Vector3 start, Vector3 end, float t) {
            while (end.y < start.y) {
                end.y = end.y + TWO_PI;
            }
            return Vector3.Lerp(start, end, t);
        }

        public static Vector3 DegreesToRads(Vector3 inputSphericalCoordsInDegrees) {
            return new Vector3(inputSphericalCoordsInDegrees.x * Mathf.Deg2Rad,
                inputSphericalCoordsInDegrees.y * Mathf.Deg2Rad,
                inputSphericalCoordsInDegrees.z);
        }
        public static Vector3 RadsToDegrees(Vector3 inputSphericalCoordsInRads) {
            return new Vector3(inputSphericalCoordsInRads.x * Mathf.Rad2Deg,
                inputSphericalCoordsInRads.y * Mathf.Rad2Deg,
                inputSphericalCoordsInRads.z);
        }

        public static Vector3 EuclideanToSpherical(Vector3 euclidean) {
            var r = euclidean.magnitude;

            var rSinTheta = euclidean.y;
            var sinTheta = rSinTheta / r;
            var theta = Mathf.Asin(sinTheta);

            // z / x = sinPhi/cosPhi
            // z / x = tan(Phi)
            // arctan(z / x) = phi
            var zOverX = euclidean.z / euclidean.x;
            var phi = Mathf.Atan(zOverX);

            return new Vector3(theta, phi, r);
        }
    }
}
