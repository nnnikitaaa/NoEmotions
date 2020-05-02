using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeSpriteRenderer : MonoBehaviour
{
    [SerializeField] Color normalColor, changedColor;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        #region Getting
        spriteRenderer = GetComponent<SpriteRenderer>();
        #endregion
    }
    private void Start()
    {
        spriteRenderer.color = normalColor;
        ChangeManager.onChange += Change;
    }
    void Change()
    {
        if (spriteRenderer.color == normalColor)
        {
            spriteRenderer.color = changedColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }
    }
}
