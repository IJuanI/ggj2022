using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public enum SwitchMode { Normal, Alt }
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
        text.text = stamina.ToString("0") + " / " + maxStamina.ToString("0");
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
        switchMode = switchMode == SwitchMode.Normal ? SwitchMode.Alt : SwitchMode.Normal;
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
