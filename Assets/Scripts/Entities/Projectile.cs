using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Damageable {

    protected override void Die() {
        base.Die();
        Destroy(gameObject);
    }
}