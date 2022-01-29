using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Skill : MonoBehaviour {

    public float cooldown = 0f;
    public bool active = true;
    float lastCastTime = -Mathf.Infinity;

    void OnValidate()
    {
        if (cooldown < 0)
            cooldown = 0;
    }

    public Transform caster { protected get; set; }

    public void TryCast(SkillsManager manager, InputAction action) {
        if (CanCast(action)) {
            Cast(manager, action);
            lastCastTime = Time.time;
        }
    }

    public virtual void Init() { }

    public virtual bool CanCast(InputAction action) {
        return active && Time.time - lastCastTime > cooldown;
    }

    public abstract void Cast(SkillsManager manager, InputAction action);

}