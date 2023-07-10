using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPannelVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject visual;
    private Vector2 _originScale;

    private void Awake()
    {
        _originScale = visual.transform.localScale;
    }
    public void SetLenScaleRefOriginLen(float scale)
    {
        visual.transform.localScale = new Vector2(_originScale.x * scale, _originScale.y);
    }
}
