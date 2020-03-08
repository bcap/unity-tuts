using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graph : MonoBehaviour
{
    private delegate float GraphFunction(float x, float z, float t);
    public enum GraphFunctionName
    {
        IdentityStatic, SquaredStatic, CubicStatic, Sine, FlyingRug, DiagonalFlyingRug
    }
    GraphFunction[] graphFunctions = {
        IdentityStatic, SquaredStatic, CubicStatic, Sine, FlyingRug, DiagonalFlyingRug
    };

    public GraphFunctionName function;
    public Transform pointPrefab;
    public int resolution;
    public float sizeFactor = 1f;

    Transform[] points = { };

    void Awake()
    {
        Rebuild();
        Update();
    }

    void Rebuild()
    {
        points = new Transform[resolution * resolution];
        float deltaX = 2f / (float)resolution;
        for (int z = 0; z < resolution; z++)
        {
            Vector3 position = new Vector3(-1, 0, (float)z / (float)resolution * 2f - 1f);
            for (int i = 0; i < resolution; i++)
            {
                Transform point = Instantiate(pointPrefab);
                point.SetParent(transform);
                position.x += deltaX;
                point.localPosition = position;
                // float deltaSize = (-Mathf.Abs((float)resolution / 2 - i) + (float)resolution / 2) / 15f;
                // point.localScale = referenceSize * (1f + deltaSize);
                points[z * resolution + i] = point;
            }
        }
    }

    void Update()
    {
        GraphFunction graphFunction = graphFunctions[(int)function];
        Vector3 referenceSize = Vector3.one / (float)resolution * 2f * sizeFactor;
        for (int z = 0; z < resolution; z++)
        {
            for (int i = 0; i < resolution; i++)
            {
                Transform point = points[z * resolution + i];
                point.localPosition = new Vector3(
                    point.localPosition.x,
                    graphFunction(point.localPosition.x, point.localPosition.z, Time.time),
                    point.localPosition.z
                );
                point.localScale = referenceSize;
            }
        }
    }


    private static float IdentityStatic(float x, float z, float t)
    {
        return x;
    }

    private static float SineStatic(float x, float z, float t)
    {
        return Mathf.Sin(Mathf.PI * x);
    }

    private static float Sine(float x, float z, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    private static float FlyingRug(float x, float z, float t)
    {
        return DiagonalFlyingRug(x, 0, t);
    }

    private static float DiagonalFlyingRug(float x, float z, float t)
    {
        float[] formulas = {
            Mathf.Sin(Mathf.PI * (x + t - z) * 1f),
            Mathf.Sin(Mathf.PI * (x + t - z) * 1.3f),
            Mathf.Sin(Mathf.PI * (x + t - z) * 1.5f),
            Mathf.Sin(Mathf.PI * (x + t - z) * 1.7f),
            Mathf.Sin(Mathf.PI * (x + t - z) * 2.13f),
        };

        float result = 0f;
        for (int i = 0; i < formulas.Length; i++)
        {
            result += formulas[i] / (float)formulas.Length;
        }
        return result;
    }

    private static float SquaredStatic(float x, float z, float t)
    {
        return x * x;
    }

    private static float CubicStatic(float x, float z, float t)
    {
        return x * x * x;
    }
}
