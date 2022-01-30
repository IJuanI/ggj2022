using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar instance;
    public Slider staminaBar;

    public float maxStamina;
    float stamina;
    
    SwitchMode switchMode;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    
    void Start() {
        stamina = maxStamina;
        switchMode = SwitchMode.Normal;
        staminaBar.maxValue = stamina * 2;
        staminaBar.value = stamina;
    }

    void Update()
    {
        stamina += Time.deltaTime * (switchMode == SwitchMode.Normal ? 1 : -1);
        if (stamina >= maxStamina * 2 || stamina <= 0)
            SkillsManager.instance.ForceCast(typeof(PhaseSkill), SkillTrigger.CastAlt);
        stamina = Mathf.Clamp(stamina, 0, maxStamina*2);
        staminaBar.value = stamina;
    }

    public void Switch(SwitchMode mode) {
        switchMode = mode;
    }

}
