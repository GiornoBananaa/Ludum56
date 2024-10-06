using UnityEngine;

namespace Utils
{
    public static class MathUtils
    {
        public static void LookAt2D(this Transform transform, Transform targetTransform)
        {
            Vector3 difference = targetTransform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }
}