using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    public static CartManager instance;

    List<CartUpgrader> carts;

    [SerializeField]
    GameObject cartPrefab, defaultCart;

    [SerializeField]
    GameObject path;

    [SerializeField]
    float CartSpeedIncrease, DefaultCartSpeed;

    float CartSpeed;

    // Start is called before the first frame update

    private void Awake()
    {
        carts = new List<CartUpgrader>();
    }
    void Start()
    {
        instance = this;
        CartSpeed = DefaultCartSpeed;
        RegisterCart(defaultCart.GetComponent<CartUpgrader>());
        UpdateCartSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            AddCart();
        }*/
    }

    public void RegisterCart(CartUpgrader cart)
    {
        carts.Add(cart);
    }

    public void CreateCart()
    {
        GameObject g = Instantiate(cartPrefab, transform.position, Quaternion.identity);
        g.GetComponent<CinemachineDollyCart>().m_Path = path.GetComponent<CinemachinePath>();
        //float trackpos = GetComponent<CinemachineDollyCart>().m_Position;
        g.GetComponent<CinemachineDollyCart>().m_Position = 0;
        RegisterCart(g.GetComponent<CartUpgrader>());
        UpdateCartSpeed();
    }

    void UpdateCartSpeed()
    {
        foreach (CartUpgrader cart in carts)
        {
            cart.SetSpeed(CartSpeed);
        }
    }

    public void UpgradeCartSpeed()
    {
        CartSpeed += CartSpeedIncrease;
        UpdateCartSpeed();
        
    }
}
