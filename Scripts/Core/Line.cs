using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour 
{
    private LineRenderer lineRender;

    public Material material;
    private List<Vector3> positionList;

    private void Awake()
    {
        positionList = new List<Vector3>();
        lineRender = GetComponent<LineRenderer>();
        lineRender.material = material;
    }

    public void Init(float startWidth, float endWidth, Color startColor, Color endColor, int numCornerVertices, int numCapVertices)
    {
        lineRender.startWidth = startWidth;
        lineRender.endWidth = endWidth;
        lineRender.startColor = startColor;
        lineRender.endColor = endColor;
        lineRender.numCornerVertices = numCornerVertices;
        lineRender.numCapVertices = numCapVertices;
    }

    public void SetProperty(int positionCount, Vector3[] positions)
    {
        lineRender.positionCount = positionCount;
        lineRender.SetPositions(positions);
    }

    public void AddPosition(Vector3 position)
    {
        positionList.Add(position);
    }

    public List<Vector3> GetPositions()
    {
        return positionList;
    }
}
