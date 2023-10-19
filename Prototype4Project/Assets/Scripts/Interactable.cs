using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    BoxCollider collider;

    Ray mouseWorldRay;

    [SerializeField]
    GameObject mesh;

    Material HighlightMat;
    
    // Start is called before the first frame update
    void Start()
    {
        HighlightMat = mesh.GetComponent<MeshRenderer>().material;
        collider = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        mouseWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (doesMouseIntersect(mouseWorldRay))
        {
            SetOutlineThickness(0.1f);
            Debug.Log("Selecting " + this);
        }
        else
        {
            SetOutlineThickness(0);
        }
    }

    bool doesMouseIntersect(Ray ray)
    {
        return collider.bounds.IntersectRay(ray);
    }

    void SetOutlineThickness(float thickness)
    {
        HighlightMat.SetFloat("_Outline_Thickness", thickness);
    }

}
