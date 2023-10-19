using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour
{
    enum VehicleState { idle, moving}
    VehicleState state;

    Vector3 targetPos, direction;

    [SerializeField]
    float speed;

    [SerializeField]
    float arrivedThresh;

    Transform targetNode;
    Transform closestNode;
    Transform previousNode;

    List<Transform> nodes = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case VehicleState.idle:
                
                break;
            case VehicleState.moving:
                moveVehicle();
                break;
        }
    }

    public void moveVehicle()
    {
        transform.position +=  direction * speed * Time.deltaTime;
        if(Vector3.Distance(transform.position, targetPos) < arrivedThresh)
        {
            updateClosestNode();
        }
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetNode = Path.instance.getClosestNode(pos);
        updateClosestNode();
        //targetPos = targetNode.position;
        //targetPos.y = transform.position.y;
        state = VehicleState.moving;
    }

    void updateClosestNode()
    {

        if (closestNode == targetNode)
        {
            state = VehicleState.idle;
        }
        //NEED TO LOOK INTO PATHFINDING maybe a star?
        getPreviousNode();
        closestNode = Path.instance.getClosestNode(transform.position, closestNode, previousNode);
        //closestNode = Path.instance.GetClosestNodeInSequence(closestNode, targetNode);
        
        //set our tagret position to be the closest nodes position
        targetPos = closestNode.position;
        //set y positions to be the same so the vehicle doesnt fly or go under ground
        targetPos.y = transform.position.y;
        //get the direction to move in
        direction = targetPos - transform.position;
        direction.Normalize();

        //nodes = Path.instance.GetPath(targetNode, closestNode);
        //transform.LookAt(closestNode.position);
        //check if we have arrived at targetnode
        Debug.Log(targetPos);
    }
    
    void getPreviousNode()
    {
        //does not work as its linear
        if(closestNode == null)
        {
            return;
        }
        int index = Path.instance.GetNodeIndex(closestNode);
        if(index == 0)
        {
            index = Path.instance.NodesLength-1;
        }
        previousNode = Path.instance.GetNodeByIndex(index-1);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(closestNode != null)
        Gizmos.DrawSphere(closestNode.position, 0.5f);
        Gizmos.color = Color.blue;
        if(targetPos != null)
        Gizmos.DrawSphere(targetPos, 0.5f);
        Gizmos.color = Color.red;
        if(targetNode != null)
        Gizmos.DrawSphere(targetNode.position, 0.5f);



        Gizmos.color = Color.green;
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
                previousNode = nodes[nodes.Count - 1].position;
            }
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }

    }
}
