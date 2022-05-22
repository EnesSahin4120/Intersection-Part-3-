using UnityEngine;

public class Mathematics : MonoBehaviour
{
    static public float Square(float grade)
    {
        return grade * grade;
    }

    static public float Distance(Coordinates coord1, Coordinates coord2)
    {
        float diffSquared = Square(coord1.x - coord2.x) +
            Square(coord1.y - coord2.y) +
            Square(coord1.z - coord2.z);
        float squareRoot = Mathf.Sqrt(diffSquared);
        return squareRoot;
    }

    static public float VectorLength(Coordinates vector)
    {
        float length = Distance(new Coordinates(0, 0, 0), vector);
        return length;
    }

    static public Coordinates Normalize(Coordinates vector)
    {
        float length = VectorLength(vector);
        vector.x /= length;
        vector.y /= length;
        vector.z /= length;

        return vector;
    }

    static public float Dot(Coordinates vector1, Coordinates vector2)
    {
        return (vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z);
    }

    static public Coordinates Projection(Coordinates vector1, Coordinates vector2)
    {
        float numeratorPart = Dot(vector1, vector2);
        float vector2Length = VectorLength(vector2);
        float denominatorPart = Square(vector2Length);
        Coordinates resultCoordinate = new Coordinates(vector2.x * (numeratorPart / denominatorPart), vector2.y * (numeratorPart / denominatorPart), vector2.z * (numeratorPart / denominatorPart));

        return resultCoordinate;
    }

    static public Vector3[] LocalToWorldVertices(GameObject shape)
    {
        MeshFilter meshfilter = shape.GetComponent<MeshFilter>();
        Mesh sharedMesh = meshfilter.sharedMesh;
        Matrix4x4 localtoWorldMatrix = shape.transform.localToWorldMatrix;
        Vector3[] resultVertices = new Vector3[sharedMesh.vertices.Length];
        for (int i = 0; i < resultVertices.Length; i++)
        {
            resultVertices[i] = localtoWorldMatrix.MultiplyPoint3x4(sharedMesh.vertices[i]);
        }
        return resultVertices;
    }

    static public AABB CalculateAABB(GameObject shape)
    {
        Vector3[] _points = LocalToWorldVertices(shape);

        float minX, minY, minZ, maxX, maxY, maxZ;
        minX = minY = minZ = Mathf.Infinity;
        maxX = maxY = maxZ = Mathf.NegativeInfinity;

        foreach (Vector3 current in _points)
        {
            if (current.x < minX) minX = current.x;
            if (current.x > maxX) maxX = current.x;
            if (current.y < minY) minY = current.y;
            if (current.y > maxY) maxY = current.y;
            if (current.z < minZ) minZ = current.z;
            if (current.z > maxZ) maxZ = current.z;
        }
        float xCenter = (minX + maxX) * 0.5f;
        float yCenter = (minY + maxY) * 0.5f;
        float zCenter = (minZ + maxZ) * 0.5f;
        Vector3 centerPos = new Vector3(xCenter, yCenter, zCenter);

        float sizeX = Mathf.Abs(minX - maxX);
        float sizeY = Mathf.Abs(minY - maxY);
        float sizeZ = Mathf.Abs(minZ - maxZ);

        AABB result = new AABB(centerPos, sizeX, sizeY, sizeZ, minX, maxX, minY, maxY, minZ, maxZ);
        return result;
    }

    static public bool IsIntersectAABB_AABB(GameObject aabbShape1, GameObject aabbShape2)
    {
        AABB aabb1 = CalculateAABB(aabbShape1);
        AABB aabb2 = CalculateAABB(aabbShape2);

        return aabb1.minX <= aabb2.maxX
            && aabb1.maxX >= aabb2.minX
            && aabb1.minY <= aabb2.maxY
            && aabb1.maxY >= aabb2.minY
            && aabb1.minZ <= aabb2.maxZ
            && aabb1.maxZ >= aabb2.minZ;
    }

    static public bool IsIntersectRay_AABB(Line ray, GameObject aabbShape)
    {
        AABB aabb = CalculateAABB(aabbShape);

        float s_Max = 0.0f;
        float t_Min = Mathf.Infinity;

        float xs, xt;
        float recipX = 1.0f / ray.v.x;
        if (recipX >= 0.0f)
        {
            xs = (aabb.minX - ray.A.x) * recipX;
            xt = (aabb.maxX - ray.A.x) * recipX;
        }
        else
        {
            xs = (aabb.maxX - ray.A.x) * recipX;
            xt = (aabb.minX - ray.A.x) * recipX;
        }

        if (xs > s_Max)
            s_Max = xs;
        if (xt < t_Min)
            t_Min = xt;
        if (s_Max > t_Min)
            return false;

        float ys, yt;
        float recipY = 1.0f / ray.v.y;
        if (recipY >= 0.0f)
        {
            ys = (aabb.minY - ray.A.y) * recipY;
            yt = (aabb.maxY - ray.A.y) * recipY;
        }
        else
        {
            ys = (aabb.maxY - ray.A.y) * recipY;
            yt = (aabb.minY - ray.A.y) * recipY;
        }

        if (ys > s_Max)
            s_Max = ys;
        if (yt < t_Min)
            t_Min = yt;
        if (s_Max > t_Min)
            return false;

        float zs, zt;
        float recipZ = 1.0f / ray.v.z;
        if (recipZ >= 0.0f)
        {
            zs = (aabb.minZ - ray.A.z) * recipZ;
            zt = (aabb.maxZ - ray.A.z) * recipZ;
        }
        else
        {
            zs = (aabb.maxZ - ray.A.z) * recipZ;
            zt = (aabb.minZ - ray.A.z) * recipZ;
        }

        if (zs > s_Max)
            s_Max = zs;
        if (zt < t_Min)
            t_Min = zt;
        if (s_Max > t_Min)
            return false;

        return true;
    }

    static public bool IsIntersectAABB_Plane(GameObject aabbShape, GameObject planeShape)
    {
        AABB aabb = CalculateAABB(aabbShape);
        Coordinates planeNormal = new Coordinates(planeShape.transform.up.x, planeShape.transform.up.y, planeShape.transform.up.z);

        Coordinates diagonalMin = new Coordinates(0, 0, 0);
        Coordinates diagonalMax = new Coordinates(0, 0, 0);
        if (planeNormal.x >= 0)
        {
            diagonalMin.x = aabb.minX;
            diagonalMax.x = aabb.maxX;
        }
        else
        {
            diagonalMin.x = aabb.maxX;
            diagonalMax.x = aabb.minX; 
        }
        if (planeNormal.y >= 0)
        {
            diagonalMin.y = aabb.minY;
            diagonalMax.y = aabb.maxY;
        }
        else
        {
            diagonalMin.y = aabb.maxY;
            diagonalMax.y = aabb.minY;
        }
        if (planeNormal.z >= 0)
        {
            diagonalMin.z = aabb.minZ;
            diagonalMax.z = aabb.maxZ;
        }
        else
        {
            diagonalMin.z = aabb.maxZ;
            diagonalMax.z = aabb.minZ;
        }

        float testNumerical = Dot(planeNormal, diagonalMin) + planeShape.transform.position.sqrMagnitude;
        if (testNumerical > 0)
            return false;

        testNumerical = Dot(planeNormal, diagonalMax) + planeShape.transform.position.sqrMagnitude;
        if (testNumerical >= 0)
            return true;
        else
            return testNumerical==0;
    }
}
