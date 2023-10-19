using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum InteractableState { unselected, selected}
    InteractableState state;

    BoxCollider collider;

    Ray mouseWorldRay;

    [SerializeField]
    GameObject mesh;

    Material HighlightMat;

    public UnityEvent<Vector3> OnClick;

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
        switch (state){
            case InteractableState.selected:
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick.Invoke(mouseWorldRay.origin);
                    if (!doesMouseIntersect(mouseWorldRay))
                    {
                        state = InteractableState.unselected;
                        SetOutlineColor(Color.white);
                    }
                }
                break;
            case InteractableState.unselected:
                if (doesMouseIntersect(mouseWorldRay))
                {
                    SetOutlineThickness(0.1f);
                }
                else
                {
                    SetOutlineThickness(0);
                }
                break;
        }
        
        
    }

    private void OnMouseDown()
    {
        state = InteractableState.selected;
        SetOutlineThickness(0.1f);
        SetOutlineColor(Color.cyan);
        
    }

    bool doesMouseIntersect(Ray ray)
    {
        return collider.bounds.IntersectRay(ray);
    }

    void SetOutlineThickness(float thickness)
    {
        HighlightMat.SetFloat("_Outline_Thickness", thickness);
    }

    void SetOutlineColor(Color color)
    {
        HighlightMat.SetColor("_Outline_Color", color);
    }
}
