using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Damageable {
    
    public float lifetime;


    void Update() {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    protected override void Die() {
        base.Die();
        Destroy(gameObject);
    }
}