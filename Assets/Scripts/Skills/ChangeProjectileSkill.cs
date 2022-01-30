using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ChangeProjectileSkill : Skill
{

    public int currentProjectileIndex = 0;
    
    List<ProjectileSkill> projectileSkills = new List<ProjectileSkill>();

    public static ChangeProjectileSkill instance;

    void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        SkillsManager manager = GetComponentsInParent<SkillsManager>()[0];
        foreach (var skillAction in manager.skillActions) {
            var skill = skillAction.skill;
            if (skill is ProjectileSkill)
                projectileSkills.Add(skill as ProjectileSkill);
        }

        currentProjectileIndex = SetActiveProjectile(currentProjectileIndex);
    }

    public override void Cast(SkillsManager manager, InputAction action)
    {
        currentProjectileIndex = SetActiveProjectile(currentProjectileIndex + 1);
    }

    int SetActiveProjectile(int index) {
        ProjectileSkill targetSkill;
        do {
            index = index % projectileSkills.Count;
            targetSkill = projectileSkills[index];
            ++index;
        } while (!targetSkill.ActivePhase());
        --index;

        foreach (var skill in projectileSkills)
            skill.active = false;
        targetSkill.active = true;

        return index;
    }

    public void UpdateProjectile() {
        currentProjectileIndex = SetActiveProjectile(0);
    }
}