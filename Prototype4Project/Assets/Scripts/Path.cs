using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path instance;

    private void Start()
    {
        instance = this;
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    public Vector3 getClosestNode(Vector3 position)
    {
        if(nodes.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 closest = nodes[0].position;
        float currentClosestDistance = Vector3.Distance(position, closest);
        for(int i = 1;i < nodes.Count;i++)
        {
            if (Vector3.Distance(nodes[i].position, closest) < currentClosestDistance)
            {
                currentClosestDistance = Vector3.Distance(nodes[i].position, closest);
                closest = nodes[i].position;
            }
        }
        return closest;
    }

    public Color lineColor;

    List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        for(int i =0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;
            if (i>0)
            {
                previousNode = nodes[i-1].position;
            }
            else if(i == 0 && nodes.Count > 1)
            {
                previousNode = nodes[nodes.Count-1].position;
            }
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawSphere(currentNode, 0.3f);
        }
    }
}
