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
            float deltaSize = (-Mathf.Abs((float)numPoints / 2 - i) + (float)numPoints / 2) / 15f;
            point.localScale = referenceSize * (1f + deltaSize);
            points[i] = point;
        }

        Update();
    }

    void Update()
    {
        GraphFunction func = PseudoChaoticWave;

        for (int i = 0; i < numPoints; i++)
        {
            Transform point = points[i];
            point.localPosition = new Vector3(
                point.localPosition.x,
                func(point.localPosition.x + Time.time),
                point.localPosition.z
            );
        }
    }

    private delegate float GraphFunction(float x);

    private float Identity(float x)
    {
        return x;
    }

    private float Sine(float x)
    {
        return Mathf.Sin(Mathf.PI * x);
    }

    private float PseudoChaoticWave(float x)
    {
        float[] formulas = {
            Mathf.Sin(Mathf.PI * x * 1f),
            Mathf.Sin(Mathf.PI * x * 1.3f),
            Mathf.Sin(Mathf.PI * x * 1.5f),
            Mathf.Sin(Mathf.PI * x * 1.7f),
            Mathf.Sin(Mathf.PI * x * 2.13f),
        };

        float result = 0f;
        for (int i = 0; i < formulas.Length; i++)
        {
            result += formulas[i] / (float)formulas.Length;
        }
        return result;
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
