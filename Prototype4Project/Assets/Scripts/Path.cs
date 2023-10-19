using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path instance;

    public int NodesLength { get { return nodes.Count; } }

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

    //need a method to find which direction down the path is easiest, then can increment nodes in 

    public Transform GetClosestNodeInSequence(Transform currentNode, Transform target)
    {
        int curIndex = GetNodeIndex(currentNode);
        int nextNodeIndex = curIndex + 1;
        int previousNodeIndex = curIndex - 1;
        if(curIndex <= 0)
        {
            previousNodeIndex = nodes.Count-1;
        }
        if(curIndex >= nodes.Count)
        {
            nextNodeIndex = 0;
        }
        if (Vector3.Distance(nodes[nextNodeIndex].position,target.position) >= Vector3.Distance(nodes[previousNodeIndex].position, target.position))
        {
            return nodes[previousNodeIndex];
        }
        return nodes[nextNodeIndex];
    }

    public Transform getClosestNode(Vector3 position)
    {
        Transform closest = nodes[0];
        float currentClosestDistance = Vector3.Distance(position, closest.position);
        for(int i = 1;i < nodes.Count;i++)
        {
            if (Vector3.Distance(nodes[i].position, position) < currentClosestDistance)
            {
                currentClosestDistance = Vector3.Distance(nodes[i].position, position);
                closest = nodes[i];
            }
        }
        return closest;
    }

    //maybe just work backwords and return a list of all the nodes i need to hit after i set a target pos
    public List<Transform> GetPath(Transform targetNode, Transform currentNode)
    {
        List<Transform> path = new List<Transform>();
        int targetIndex = GetNodeIndex(targetNode);
        int currentIndex = GetNodeIndex(currentNode);
        if(targetIndex > currentIndex)
        {
            for (int i = targetIndex; i > currentIndex; i--)
            {
                path.Add(nodes[i]);
            }
        }
        else if(targetIndex < currentIndex)
        {
            for (int i = targetIndex; i < currentIndex; i++)
            {
                path.Add(nodes[i]);
            }
        }
        return path;
    }

    public int GetNodeIndex(Transform node)
    {
        int index = -1;
        for (int i = 0;i < nodes.Count;i++)
        {
            if (nodes[i] == node)
            {
                index = i; break;
            }
        }
        return index;
    }

    public Transform GetNodeByIndex(int index)
    {
        return nodes[index];
    }

    public Transform getClosestNode(Vector3 position, Transform currentNode, Transform previousNode)
    {
        int currentNodeIndex = GetNodeIndex(currentNode);
        if(currentNodeIndex >= nodes.Count-1)
        {
            currentNodeIndex = -1;
        }
        Transform closest = nodes[currentNodeIndex+1];
        float currentClosestDistance = Vector3.Distance(position, closest.position);
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i] == currentNode)
            {
                continue;
            }
            if(previousNode != null)
            {
                if (nodes[i] == previousNode)
                {
                    continue;
                }
            }
            if (Vector3.Distance(nodes[i].position, position) < currentClosestDistance)
            {
                currentClosestDistance = Vector3.Distance(nodes[i].position, position);
                closest = nodes[i];
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
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
}
