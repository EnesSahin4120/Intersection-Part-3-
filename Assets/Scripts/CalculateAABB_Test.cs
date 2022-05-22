using UnityEngine;

public class CalculateAABB_Test : MonoBehaviour
{
    [SerializeField] private GameObject shape;

    private void Start()
    {
        AABB aabb = Mathematics.CalculateAABB(shape);

        Debug.DrawLine(new Vector3(aabb.minX, aabb.minY, aabb.minZ), new Vector3(aabb.maxX, aabb.minY, aabb.minZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.minY, aabb.minZ), new Vector3(aabb.minX, aabb.maxY, aabb.minZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.maxY, aabb.minZ), new Vector3(aabb.maxX, aabb.maxY, aabb.minZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.maxX, aabb.maxY, aabb.minZ), new Vector3(aabb.maxX, aabb.minY, aabb.minZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.maxY, aabb.minZ), new Vector3(aabb.minX, aabb.maxY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.maxY, aabb.maxZ), new Vector3(aabb.maxX, aabb.maxY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.maxX, aabb.maxY, aabb.maxZ), new Vector3(aabb.maxX, aabb.maxY, aabb.minZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.maxX, aabb.minY, aabb.minZ), new Vector3(aabb.maxX, aabb.minY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.maxX, aabb.maxY, aabb.maxZ), new Vector3(aabb.maxX, aabb.minY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.minY, aabb.minZ), new Vector3(aabb.minX, aabb.minY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.minY, aabb.maxZ), new Vector3(aabb.minX, aabb.maxY, aabb.maxZ), Color.red, Mathf.Infinity);
        Debug.DrawLine(new Vector3(aabb.minX, aabb.minY, aabb.maxZ), new Vector3(aabb.maxX, aabb.minY, aabb.maxZ), Color.red, Mathf.Infinity);

    }
}
