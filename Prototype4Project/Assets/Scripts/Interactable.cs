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
                    deselect();
                    invokeClickEvent();
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

    void invokeClickEvent()
    {
        RaycastHit hit;
        if (Physics.Raycast(mouseWorldRay.origin, mouseWorldRay.direction, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(mouseWorldRay.origin, mouseWorldRay.direction * hit.distance, Color.yellow);
        }
        OnClick.Invoke(mouseWorldRay.origin + mouseWorldRay.direction * hit.distance);
    }

    void deselect()
    {
        if (!doesMouseIntersect(mouseWorldRay))
        {
            state = InteractableState.unselected;
            SetOutlineColor(Color.white);
        }
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseUp()
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
