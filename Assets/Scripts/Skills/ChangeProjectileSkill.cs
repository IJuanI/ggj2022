using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeProjectileSkill : Skill
{

    public int currentProjectileIndex = 0;
    
    List<Skill> projectileSkills = new List<Skill>();

    void Start()
    {
        SkillsManager manager = GetComponentsInParent<SkillsManager>()[0];
        foreach (var skillAction in manager.skillActions) {
            var skill = skillAction.skill;
            if (skill is ProjectileSkill)
                projectileSkills.Add(skill);
        }

        SetActiveProjectile(currentProjectileIndex);
    }

    public override void Cast(SkillsManager manager, InputAction action)
    {
        currentProjectileIndex = (currentProjectileIndex + 1) % projectileSkills.Count;
        SetActiveProjectile(currentProjectileIndex);
    }

    void SetActiveProjectile(int index) {
        foreach (var skill in projectileSkills)
            skill.active = false;
        projectileSkills[index].active = true;
    }
}