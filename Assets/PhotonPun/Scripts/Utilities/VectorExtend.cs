using UnityEngine;

public static class VectorExtend
{
    public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(value.x, min.x, max.x);
        float y = Mathf.Clamp(value.y, min.y, max.y);
        float z = Mathf.Clamp(value.z, min.z, max.z);
        return new Vector3(x, y, z);
    }
}