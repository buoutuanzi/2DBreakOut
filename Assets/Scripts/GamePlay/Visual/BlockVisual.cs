using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVisual : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    const string CRACKPROPERTYNAME = "_CrackScale";
    // Start is called before the first frame update
    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
    }

    // Update is called once per frame
    public void UpdateCrackVisual(float percent)
    {
        materialPropertyBlock.SetFloat(CRACKPROPERTYNAME, percent);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
