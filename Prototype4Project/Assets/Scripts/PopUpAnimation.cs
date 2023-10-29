using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpAnimation : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;

    Vector3 origin;

    private TextMeshProUGUI tmp;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one *scaleCurve.Evaluate(time);
        transform.position = origin + new Vector3(0, 1+ heightCurve.Evaluate(time), 0);
        time += Time.deltaTime;
        if(time > 2)
        {
            Destroy(this.gameObject);
        }
    }
}
