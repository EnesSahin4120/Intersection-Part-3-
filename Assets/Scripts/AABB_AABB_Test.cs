using UnityEngine;

public class AABB_AABB_Test : MonoBehaviour
{
    private bool isIntersect;
    [SerializeField] private GameObject aabbShape1;
    [SerializeField] private GameObject aabbShape2;

    private void Update()
    {
        isIntersect = Mathematics.IsIntersectAABB_AABB(aabbShape1, aabbShape2);

        if (!isIntersect)
            SetColor(aabbShape1, Color.green);
        else
            SetColor(aabbShape1, Color.red);
    }

    private void SetColor(GameObject shape, Color color)
    {
        shape.GetComponent<MeshRenderer>().material.color = color;
    }
}
