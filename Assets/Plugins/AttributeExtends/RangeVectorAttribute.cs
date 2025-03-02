using UnityEngine;

public sealed class RangeVectorAttribute : PropertyAttribute
{
    public Vector3 min;
    public Vector3 max;

    public RangeVectorAttribute(float minX, float minY, float maxX, float maxY)
    {
        min = new Vector2(minX, minY);
        max = new Vector2(maxX, maxY);
    }

    public RangeVectorAttribute(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
    {
        min = new Vector3(minX, minY, minZ);
        max = new Vector3(maxX, maxY, maxZ);
    }
}