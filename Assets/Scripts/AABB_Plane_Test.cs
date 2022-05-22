using UnityEngine;

public class AABB_Plane_Test : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject cube;

    private bool isIntersect;

    private void Update() 
    {
        isIntersect = Mathematics.IsIntersectAABB_Plane(cube, plane);

        if (isIntersect)
            SetColor(cube, Color.red);
        else
            SetColor(cube, Color.green);
    }

    private void SetColor(GameObject shape, Color color)
    {
        shape.GetComponent<MeshRenderer>().material.color = color;
    }
}
