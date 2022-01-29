using System;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchMode { Normal, Alt }

[Serializable]
public struct SwitchSkillColor {
    public SwitchMode mode;
    public Color color;
    public int targetLayer;
}

public class SwitchSkill : Skill
{
    public List<SwitchSkillColor> modeSettings;

    Dictionary<SwitchMode, Color> colorMap;
    Dictionary<SwitchMode, int> layerMap;
    SwitchMode currentMode;
    SpriteRenderer casterRender;

    void OnValidate()
    {
        if (modeSettings == null) return;

        colorMap = new Dictionary<SwitchMode, Color>();
        layerMap = new Dictionary<SwitchMode, int>();
        foreach (var color in modeSettings) {
            colorMap[color.mode] = color.color;
            layerMap[color.mode] = color.targetLayer;
        }
    }

    public override void Init()
    {
        currentMode = SwitchMode.Normal;
        casterRender = caster.GetComponent<SpriteRenderer>();
        UpdateCaster();
    }

    public override void Cast()
    {
        Switch();
        UpdateCaster();
    }

    void Switch() {
        currentMode = currentMode == SwitchMode.Normal ? SwitchMode.Alt : SwitchMode.Normal;
        WaveEffect.Play(caster.transform.position);
    }

    void UpdateCaster() {
        casterRender.color = colorMap[currentMode];
        caster.gameObject.layer = layerMap[currentMode];
    }
}