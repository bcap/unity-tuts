using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    public int numPoints;

    Transform[] points;
    Vector3 referenceSize;

    void Awake()
    {
        points = new Transform[numPoints];
        referenceSize = Vector3.one / (float)numPoints * 2f;

        Vector3 position = new Vector3(-1, 0, 0);
        float deltaX = 2f / (float)numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.SetParent(transform);
            position.x += deltaX;
            point.localPosition = position;
            point.localScale = referenceSize;
            points[i] = point;
        }
    }

    void Update()
    {
        GraphFunction func = SinePi;

        // define positions
        for (int i = 0; i < numPoints; i++)
        {
            Transform point = points[i];
            point.localPosition = new Vector3(
                point.localPosition.x,
                func(point.localPosition.x + Time.time),
                point.localPosition.z
            );
            float deltaSize = (-Mathf.Abs((float)numPoints / 2 - i) + (float)numPoints / 2) / 20f;
            point.localScale = referenceSize * (1f + deltaSize);
        }
    }

    private delegate float GraphFunction(float x);

    private float Identity(float x)
    {
        return x;
    }

    private float Sine(float x)
    {
        return Mathf.Sin(x);
    }

    private float SinePi(float x)
    {
        return Sine(Mathf.PI * x);
    }

    private float Squared(float x)
    {
        return x * x;
    }

    private float Cubic(float x)
    {
        return x * x * x;
    }
}
