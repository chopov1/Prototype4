using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour
{
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetPos = Path.instance.getClosestNode(pos);
        Debug.Log(targetPos);
    }
}
