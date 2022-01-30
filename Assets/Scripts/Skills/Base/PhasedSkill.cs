using UnityEngine.InputSystem;

public abstract class PhasedSkill : Skill {

    public bool singleMode = false;
    public SwitchMode targetMode;

    public override bool CanCast(InputAction action) {
        if (!base.CanCast(action)) return false;
        return ActivePhase();
    }

    public bool ActivePhase() {
        return !singleMode || PhaseSkill.PlayerMode == targetMode;
    }
}