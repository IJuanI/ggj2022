using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public static HealthBar instance;

    public TMP_Text text;
    public Slider healthBar;

    float playerMaxHealth;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start()
    {
        playerMaxHealth = PlayerMotor.player.GetComponent<Damageable>().maxHealth;
        UpdateLife(playerMaxHealth);
    }

    
    public void UpdateLife(float life)
    {
        float lifePercentage = life / playerMaxHealth;
        healthBar.value = lifePercentage;
        text.text = life.ToString("0") + " / " + playerMaxHealth.ToString("0");
    }
}
