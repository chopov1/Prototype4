using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    public static PopUpSpawner Instance;

    [SerializeField]
    GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPopUp(Vector3 pos, Quaternion rot, string text)
    {
        GameObject g =Instantiate(prefab, pos, rot);
        g.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
