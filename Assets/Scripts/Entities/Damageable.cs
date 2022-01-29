using UnityEngine;

public class Damageable : MonoBehaviour {

    public LayerMask damagerMask;
    public float maxHealth = 10f;
    public float invincibilityTime = 0f;

    protected float health;
    protected float lastHitTime = -Mathf.Infinity;

    void Start()
    {
        health = maxHealth;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        OnHit(other.gameObject.GetComponent<Damager>());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        OnHit(other.gameObject.GetComponent<Damager>());
    }

    void OnHit(Damager damager) {
        if (damager == null) return;
        if (damagerMask.value != (damagerMask.value | (1 << damager.gameObject.layer))) return;

        Damage(damager.damage);
    }

    protected virtual void Damage(float amount) {
        if (Time.time < lastHitTime + invincibilityTime) return;
        health -= amount;
        lastHitTime = Time.time;
        if (health < 0) { Die(); }
    }

    protected virtual void Die() {

    }

}