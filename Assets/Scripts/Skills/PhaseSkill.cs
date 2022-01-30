using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SwitchMode { Normal, Alt }

[Serializable]
public struct SwitchSkillColor {
    public SwitchMode mode;
    public Color color;
    public int targetLayer;
}

public class PhaseSkill : Skill
{
    public List<SwitchSkillColor> modeSettings;

    public static SwitchMode PlayerMode = SwitchMode.Normal;

    Dictionary<SwitchMode, Color> colorMap;
    Dictionary<SwitchMode, int> layerMap;
    SwitchMode currentMode;
    SpriteRenderer casterRender;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (modeSettings == null) return;

        colorMap = new Dictionary<SwitchMode, Color>();
        layerMap = new Dictionary<SwitchMode, int>();
        foreach (var color in modeSettings) {
            colorMap[color.mode] = color.color;
            layerMap[color.mode] = color.targetLayer;
        }
    }

    protected override void Start() {
        base.Start();
        OnValidate();
    }

    public override void Init()
    {
        currentMode = SwitchMode.Normal;
        casterRender = caster.GetComponentInChildren<SpriteRenderer>();
        UpdateCaster();
    }

    public override void Cast(SkillsManager manager, InputAction action)
    {
        Switch();
        UpdateCaster();
    }

    void Switch() {
        Logger.Log("[Phase] switching");
        currentMode = currentMode == SwitchMode.Normal ? SwitchMode.Alt : SwitchMode.Normal;
        PlayerMode = currentMode;
        StaminaBar.instance.Switch(currentMode);
        WaveEffect.Play(caster.transform.position);
        ChangeProjectileSkill.instance.UpdateProjectile();
    }

    void UpdateCaster() {
        casterRender.color = colorMap[currentMode];
        caster.gameObject.layer = layerMap[currentMode];
    }
}