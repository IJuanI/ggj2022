using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SkillTrigger { CastAlt }

public class SkillsManager : MonoBehaviour {

    public static Dictionary<SkillTrigger, string> triggerNames = new Dictionary<SkillTrigger, string> {
        { SkillTrigger.CastAlt, "Cast Alt" }
    };

    public List<SkillAction> skillActions;

    void Start()
    {
        foreach (var action in skillActions) {
            action.skill.caster = transform;
            action.skill.Init();
        }
    }

    public void OnCast(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;

        skillActions.Where(action => ctx.action.name == triggerNames[action.trigger])
            .ToList().ForEach(action => action.skill.TryCast(ctx.action));
    }

}