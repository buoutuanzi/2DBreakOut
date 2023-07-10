using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCollectableVisual : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer itemSpriteRenderer;
    private BuffCollectable buffCollectable;
    private Color _curColor = BuffCollectableVisualConfig.DefaultColor;
    private void Awake()
    {
        buffCollectable = GetComponent<BuffCollectable>();
    }

    public void UpdateVisual()
    {
        BuffType buff = buffCollectable.buffType;
        Color targetColor = BuffCollectableVisualConfig.DefaultColor;
        if (BuffCollectableVisualConfig.IsHasConfig(buff))
        {
            targetColor = BuffCollectableVisualConfig.GetVisualColorByBuff(buff);
        }

        UpdateColor(targetColor);   
    }

    private void UpdateColor(Color targetColor)
    {
        if(targetColor == _curColor)
        {
            return;
        }

        itemSpriteRenderer.color = targetColor;
    }
}