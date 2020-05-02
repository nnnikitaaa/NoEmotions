using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeManager : MonoBehaviour
{
    public static Action onChange;

    [SerializeField] float changeTime;
    Transform healthBar;
    Slider healthSlider;
    bool inside;
    private void Awake()
    {
        #region Getting
        healthBar = transform.Find("Health Bar");
        healthSlider = Instantiate(GameManager.instance.healthSliderPrefab, healthBar.position, Quaternion.identity);
        #endregion
    }
    private void Start()
    {
        gameObject.layer = GameManager.PLAYER_NORMAL_LAYER;
    }
    private void Update()
    {
        healthSlider.transform.position = healthBar.position;
        if (inside)
        {
            healthSlider.value -= 1 / changeTime * Time.deltaTime;
            if (healthSlider.value == 0)
            {
                ActualChange();
            }
        }
    }
    public void Change()
    {
        if (healthSlider.value > 0)
        {
            ActualChange();
        }
    }
    void ActualChange()
    {
        inside = !inside;
        onChange?.Invoke();
        if (inside)
        {
            gameObject.layer = GameManager.PLAYER_CHANGED_LAYER;
        }
        else
        {
            gameObject.layer = GameManager.PLAYER_NORMAL_LAYER;
        }
    }
}
