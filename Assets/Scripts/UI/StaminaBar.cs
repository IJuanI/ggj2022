using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum SwitchMode { Normal, Alt }

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar instance;
    public TMP_Text text;
    public Slider staminaBar;

    float maxStamina;
    public float stamina;
    public float dValue;

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

    
    void Start(){
        switchMode = SwitchMode.Normal;
        maxStamina = stamina * 2;
        UpdateEnergy(stamina);
    }

    public void UpdateEnergy(float energy){
        staminaBar.maxValue = maxStamina;
        staminaBar.value = energy;
        text.text = life.ToString("0") + " / " + playerMaxHealth.ToString("0");
    }

    void Update()
    {
        if( switchMode == SwitchMode.Normal && stamina < maxStamina ){
            IncreaseStamina();
        }else if( switchMode == SwitchMode.Alt && stamina > 0 ){
            DecreaseStamina();
        }
    }

    public void Switch() {
        currentMode = currentMode == SwitchMode.Normal ? SwitchMode.Alt : SwitchMode.Normal;
    }

    private void DecreaseStamina(){
        if(stamina != 0){
            stamina -= dValue * Time.deltaTime;
        }
    }
    private void IncreaseStamina(){
        if(stamina != maxStamina){
            stamina += dValue * Time.deltaTime;
        }
    }

}
