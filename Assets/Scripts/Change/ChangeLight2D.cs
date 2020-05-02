using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class ChangeLight2D : MonoBehaviour
{
    [SerializeField] Color normalColor, changedColor;
    Light2D light2D;
    private void Awake()
    {
        #region Getting
        light2D = GetComponent<Light2D>();
        #endregion
    }
    private void Start()
    {
        light2D.color = normalColor;
        ChangeManager.onChange += Change;
    }
    void Change()
    {
        if (light2D.color == normalColor)
        {
            light2D.color = changedColor;
        }
        else
        {
            light2D.color = normalColor;
        }
    }
}
