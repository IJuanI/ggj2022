using UnityEngine;

public class Damageable : MonoBehaviour {

    public LayerMask damagerMask;
    public float health = 10f;
    public float invincibilityTime = 0f;

    float lastHitTime = -Mathf.Infinity;
    
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

    void Damage(float amount) {
        if (Time.time < lastHitTime + invincibilityTime) return;
        health -= amount;
        lastHitTime = Time.time;
    }

}