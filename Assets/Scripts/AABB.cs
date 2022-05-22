using UnityEngine;

public class AABB : MonoBehaviour
{
    public Vector3 center;
    public float sizeX;
    public float sizeY;
    public float sizeZ;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ; 


    public AABB(Vector3 _center,float _sizeX, float _sizeY, float _sizeZ, float _minX, float _maxX, float _minY, float _maxY, float _minZ, float _maxZ)  
    {
        center = _center;
        sizeX = _sizeX;
        sizeY = _sizeY;
        sizeZ = _sizeZ;
        minX = _minX;
        minY = _minY;
        minZ = _minZ;
        maxX = _maxX;
        maxY = _maxY;
        maxZ = _maxZ;
    }
}
