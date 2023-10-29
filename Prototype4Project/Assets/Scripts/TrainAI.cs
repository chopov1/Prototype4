using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAI : MonoBehaviour
{
    enum VehicleState { idle, moving }
    VehicleState state;

    Vector3 targetPos, direction;

    [SerializeField]
    float speed;

    [SerializeField]
    float arrivedThresh;

    Transform targetNode;
    Transform currentNode;
    // Start is called before the first frame update
    void Start()
    {
        currentNode = TrainLine.instance.GetClosestTarget(transform.position);
        float ypos = transform.position.y;
        transform.position = currentNode.position;
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case VehicleState.idle:

                break;
            case VehicleState.moving:
                moveVehicle();
                break;
        }
    }

    public float maxAngularVelocity = 90f; // Maximum turning speed in degrees per second

    // Move the object from its current position to the target position
    public void MoveTo(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the angle to turn to face the target
        float angleToTarget = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        // Calculate the time it takes to turn to face the target
        float timeToTurn = Mathf.Abs(angleToTarget) / maxAngularVelocity;

        // Rotate the object towards the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), maxAngularVelocity * Time.deltaTime);

        // Move the object forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // Check if the turn is complete
        if (timeToTurn < Time.deltaTime)
        {
            // Snap to the exact target direction to avoid overshooting
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void moveVehicle()
    {
        //transform.position += direction * speed * Time.deltaTime;
        MoveTo(targetPos);
        if (Vector3.Distance(transform.position, targetPos) < arrivedThresh)
        {
            if (TrainLine.instance.HasReachedTarget(targetNode, currentNode))
            {
                state = VehicleState.idle;
            }
            updateTargetPos();
            
        }
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetNode = TrainLine.instance.GetClosestTarget(pos);
        updateTargetPos();
        state = VehicleState.moving;
    }

    void updateTargetPos()
    {
        currentNode = TrainLine.instance.GetNextNode(currentNode, targetNode);
        targetPos = currentNode.position;
        targetPos.y = transform.position.y;
        direction = targetPos - transform.position;
        direction.Normalize();
        //transform.LookAt(targetPos);
    }

    private void OnDrawGizmos()
    {
        if(currentNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentNode.position, 0.5f);
        }
        if(targetNode != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetNode.position, 0.5f);
        }
        
    }
}
