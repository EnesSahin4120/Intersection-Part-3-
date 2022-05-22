using UnityEngine;

public class Ray_AABB_Test : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform rayDirection;

    private bool isIntersect; 

    private void Update()
    {
        Coordinates rayOriginCoord = new Coordinates(rayOrigin.position.x, rayOrigin.position.y, rayOrigin.position.z);
        Coordinates rayDirCoord = new Coordinates(rayDirection.position.x, rayDirection.position.y, rayDirection.position.z);
        Line _ray = new Line(rayOriginCoord, rayDirCoord, Line.LINETYPE.RAY);

        isIntersect = Mathematics.IsIntersectRay_AABB(_ray, cube);

        if (isIntersect)
            Debug.DrawLine(rayOriginCoord.ToVector(), rayDirCoord.ToVector(), Color.red);
        else
            Debug.DrawLine(rayOriginCoord.ToVector(), rayDirCoord.ToVector(), Color.green);
    }
}
