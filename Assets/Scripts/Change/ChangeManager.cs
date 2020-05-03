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
        onChange = null;
        #endregion
    }
    private void Start()
    {
        healthSlider = Instantiate(GameManager.instance.healthSliderPrefab, healthBar.position, Quaternion.identity);
        gameObject.layer = GameManager.PLAYER_NORMAL_LAYER;
    }
    private void Update()
    {
        if (healthSlider)
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
    }
    public void RestoreHealth()
    {
        if (healthSlider)
            healthSlider.value = healthSlider.maxValue;
    }
    public void Change()
    {
        if (healthSlider)
        {
            if (healthSlider.value > 0)
            {
                ActualChange();
            }
        }
    }
    public void Die()
    {
        GameObject explosion = Instantiate(GameManager.instance.deathExplosion, transform.position, Quaternion.identity);
        if (inside)
        {
            ParticleSystem.MainModule mainModule = explosion.GetComponent<ParticleSystem>().main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(Color.black);
        }
        Destroy(healthSlider.gameObject);

        Destroy(explosion, 3f);
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
        SoudManager.instance.Play("Change");
    }
}
