using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileSkill : Skill {

    public float velocity;
    public float spawnOffset;
    public Projectile projectile;
    public Color gizmoColor;

    public override void Cast(InputAction action)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseViewportPos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector2 dir = mouseViewportPos - (Vector2)Camera.main.WorldToViewportPoint(caster.position);
        dir.Normalize();

        Vector2 spawnPos = caster.transform.position + (Vector3)dir * spawnOffset;
        Projectile newProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = dir * velocity;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, spawnOffset);
    }
}