using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Skill : MonoBehaviour {


    public float cooldown = 0f;
    public bool active = true;
    float lastCastTime = 0;

    protected virtual void OnValidate()
    {
        if (cooldown < 0)
            cooldown = 0;
        lastCastTime = -cooldown;
    }

    protected virtual void Start() { OnValidate(); }

    public Transform caster { protected get; set; }

    public void TryCast(SkillsManager manager, InputAction action) {
        Logger.Log("[Skill] time: " + Time.time);
        if (CanCast(action)) {
            Cast(manager, action);
            Logger.Log("[Skill] casted");

            lastCastTime = Time.time;
        }
    }

    public virtual void Init() { }

    public virtual bool CanCast(InputAction action) {
        return active && Time.time - lastCastTime > cooldown;
    }

    public abstract void Cast(SkillsManager manager, InputAction action);

}