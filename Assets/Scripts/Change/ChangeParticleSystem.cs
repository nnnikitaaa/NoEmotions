using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ChangeParticleSystem : MonoBehaviour
{
    [SerializeField] Color normalColor, changedColor;
    ParticleSystem.MainModule settings;
    private void Awake()
    {
        #region Getting

        settings = GetComponent<ParticleSystem>().main;

        #endregion
    }
    private void Start()
    {
        settings.startColor = new ParticleSystem.MinMaxGradient(normalColor);
        ChangeManager.onChange += Change;
    }
    void Change()
    {
        if (settings.startColor.color == normalColor)
        {
            settings.startColor = new ParticleSystem.MinMaxGradient(changedColor);
        }
        else
        {
            settings.startColor = new ParticleSystem.MinMaxGradient(normalColor);
        }
    }
}
