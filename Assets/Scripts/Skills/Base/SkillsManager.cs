using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SkillTrigger { CastAlt, CastMain, CastUtility }

public class SkillsManager : MonoBehaviour {

    public static Dictionary<SkillTrigger, string> triggerNames = new Dictionary<SkillTrigger, string> {
        { SkillTrigger.CastAlt, "Cast Alt" },
        { SkillTrigger.CastMain, "Cast Main" },
        { SkillTrigger.CastUtility, "Cast Utility" }
    };
    public static SkillsManager instance;

    public List<SkillAction> skillActions;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start()
    {
        foreach (var action in skillActions) {
            action.skill.caster = transform;
            action.skill.Init();
        }
    }

    public void OnCast(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        Logger.Log("[SkillsManager] casting " + ctx.action.name);

        skillActions.Where(action => ctx.action.name == triggerNames[action.trigger])
            .ToList().ForEach(action => action.skill.TryCast(this, ctx.action));
    }

    public void ForceCast(System.Type type, SkillTrigger trigger) {
        skillActions.Where(action => action.skill.GetType() == type && action.trigger == trigger)
            .ToList().ForEach(action => action.skill.Cast(this, null));
    }

}