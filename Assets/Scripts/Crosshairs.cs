using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;
    [SerializeField]
    SpriteRenderer dot;
    [SerializeField]
    Color dotHighlightColor;
    Color originalDotColor;

    void Start()
    {
        Cursor.visible = false;
        originalDotColor = dot.color;
    }

    void Update()
    {
        transform.Rotate(40 * Time.deltaTime * Vector3.forward);
    }

    public void DetectTargets(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            dot.color = dotHighlightColor;
        }
        else
        {
            dot.color = originalDotColor;
        }
    }
}
