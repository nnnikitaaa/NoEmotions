using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ChangeImage : MonoBehaviour
{
    [SerializeField] Color normalColor, changedColor;
    Image image;
    private void Awake()
    {
        #region Getting

        image = GetComponent<Image>();

        #endregion
    }
    private void Start()
    {
        image.color = normalColor;
        ChangeManager.onChange += Change;

    }
    void Change()
    {
        if (image.color == normalColor)
        {
            image.color = changedColor;
        }
        else
        {
            image.color = normalColor;
        }
    }
}
