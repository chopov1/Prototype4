using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CartUpgrader : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //CartManager.instance.RegisterCart(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float speed)
    {
        GetComponent<CinemachineDollyCart>().m_Speed = speed;
    }

}
