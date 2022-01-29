using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Skill : MonoBehaviour {

    public float cooldown = 0f;
    float lastCastTime = -Mathf.Infinity;

    void OnValidate()
    {
        if (cooldown < 0)
            cooldown = 0;
    }

    public Transform caster { protected get; set; }

    public void TryCast(InputAction action) {
        if (CanCast(action)) {
            Cast(action);
            lastCastTime = Time.time;
        }
    }

    public virtual void Init() { }

    public virtual bool CanCast(InputAction action) {
        return Time.time - lastCastTime > cooldown;
    }

    public abstract void Cast(InputAction action);

}