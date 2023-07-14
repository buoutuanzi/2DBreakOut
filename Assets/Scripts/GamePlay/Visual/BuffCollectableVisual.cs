using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCollectableVisual : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer itemSpriteRenderer;

    public void UpdateVisual(BuffItemSingleVisualConfig config)
    {
        if (config != null)
        {
            itemSpriteRenderer.sprite = config.sprite;
        }  
    }
}