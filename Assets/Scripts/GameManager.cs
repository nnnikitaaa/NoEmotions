using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GROUND_LAYER;
    public static int GROUND_LAYERMASK;
    public static int PLAYER_NORMAL_LAYER;
    public static int PLAYER_CHANGED_LAYER;
    public static GameManager instance { get; set; }

    [SerializeField] Slider healthSlider;
    public Slider healthSliderPrefab { get { return healthSlider; } }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GROUND_LAYER = LayerMask.NameToLayer("Ground");
        GROUND_LAYERMASK = (int)Mathf.Pow(2, GROUND_LAYER);
        PLAYER_CHANGED_LAYER = LayerMask.NameToLayer("PlayerChanged");
        PLAYER_NORMAL_LAYER = LayerMask.NameToLayer("PlayerNormal");

    }
}
