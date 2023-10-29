using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    GameObject speedButtonObj, cartButtonObj, MoneyObj, valueObj;
    [SerializeField]
    Transform MoneyPopupPrefab;

    TMPro.TextMeshProUGUI speedButtonTxt, cartButtonTxt, MoneyTxt, valueTxt;

    [SerializeField]
    float speedCost, cartCost, valueCost, startingMoney;

    float Money, MoneyPerDelivery;
    // Start is called before the first frame update
    void Start()
    {
        MoneyTxt = MoneyObj.GetComponent<TMPro.TextMeshProUGUI>();
        speedButtonTxt = speedButtonObj.GetComponent<TMPro.TextMeshProUGUI>();
        cartButtonTxt = cartButtonObj.GetComponent<TMPro.TextMeshProUGUI>();
        valueTxt = valueObj.GetComponent<TMPro.TextMeshProUGUI>();
        valueTxt.text = "Delivery Value+ " + valueCost + "$"; 
        speedButtonTxt.text = "Speed+ " + speedCost + "$";
        cartButtonTxt.text = "Cart+ " + cartCost + "$";
        MoneyPerDelivery = 1;
        Money = startingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuySpeed()
    {
        if(Money >= speedCost)
        {
            CartManager.instance.UpgradeCartSpeed();
            AddMoney(-speedCost);
        }
    }

    public void BuyCart()
    {
        if (Money >= cartCost)
        {
            CartManager.instance.CreateCart();
            AddMoney(-cartCost);
        }
    }

    public void BuyValue()
    {
        if (Money >= valueCost)
        {
            MoneyPerDelivery++;
            AddMoney(-valueCost);
        }
    }

    private void AddMoney(float money)
    {
        Money += money;
        MoneyTxt.text = Money + "$";
    }

    private void OnTriggerEnter(Collider other)
    {
        AddMoney(MoneyPerDelivery);
        SpawnMoney();
    }

    private void SpawnMoney()
    {
        PopUpSpawner.Instance.SpawnPopUp(transform.position + new Vector3(-10, 20, 0), Quaternion.LookRotation(Vector3.right, Vector3.up), "+" + MoneyPerDelivery + "$");
    }
}
