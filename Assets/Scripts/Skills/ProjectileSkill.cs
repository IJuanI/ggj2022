using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileSkill : Skill {

    public float velocity;
    public float spawnOffset;
    public Projectile projectile;
    public Color gizmoColor;

    public override void Cast(SkillsManager manager, InputAction action)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseViewportPos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector2 dir = mouseViewportPos - (Vector2)Camera.main.WorldToViewportPoint(caster.position);
        dir.Normalize();

        Vector3 xzDir = new Vector3(dir.x, 0, dir.y);
        Vector3 spawnPos = caster.position + xzDir * spawnOffset;
        Projectile newProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        newProjectile.gameObject.layer = caster.gameObject.layer;
        newProjectile.GetComponent<Rigidbody>().velocity = xzDir * velocity;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, spawnOffset);
    }
}