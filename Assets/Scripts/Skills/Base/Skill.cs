using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Skill : MonoBehaviour {

    public Transform caster { protected get; set; }

    public void TryCast(InputAction action) {
        if (CanCast(action)) {
            Cast();
        }
    }

    public virtual void Init() { }

    public virtual bool CanCast(InputAction action) { return true; }

    public abstract void Cast();

}