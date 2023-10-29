using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLine : MonoBehaviour
{
    public static TrainLine instance;
    public int NodesLength { get { return nodes.Count; } }

    public Color lineColor;

    List<Transform> nodes = new List<Transform>();

    private void Awake()
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

    public Transform GetStation(bool start)
    {
        if(start)
        {
            return nodes[0];
        }
        return nodes[nodes.Count - 1];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform GetClosestTarget(Vector3 pos)
    {
        float node0dis = Vector3.Distance(nodes[0].position,pos);
        float nodeEnddis = Vector3.Distance(nodes[nodes.Count-1].position, pos);
        if (node0dis > nodeEnddis)
        {
            return nodes[nodes.Count - 1];
        }
        return nodes[0];
    }

    public bool HasReachedTarget(Transform target, Transform currentNode)
    {
        if(currentNode == target)
        {
            return true;
        }
        return false;
    }

    public Transform GetNextNode(Transform currentNode, Transform target)
    {
        int curindex = GetNodeIndex(currentNode);
        int targetIndex = GetNodeIndex(target);
        if(curindex > targetIndex)
        {
            return nodes[curindex - 1];
        }
        else if(curindex < targetIndex)
        {
            return nodes[curindex+1];
        }
        else
        {
            return target;
        }

    }

    public int GetNodeIndex(Transform node)
    {
        int index = -1;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == node)
            {
                index = i; break;
            }
        }
        return index;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;
            if (i > 0)
            {
                previousNode = nodes[i - 1].position;
            }
            else if (i == 0 && nodes.Count > 1)
            {
                previousNode = nodes[0].position;
            }
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
}
