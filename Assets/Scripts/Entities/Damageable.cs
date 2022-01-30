using System.Collections;
using System.Linq;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public LayerMask damagerMask;
    public float dieDelay = 0f;
    public float maxHealth = 10f;
    public float invincibilityTime = 0f;
    public GameObject mainObject;
    public Animator animator;

    [SerializeField]
    protected float health;
    protected float lastHitTime = -Mathf.Infinity;

    void Start()
    {
        health = maxHealth;
    }
    
    void OnTriggerEnter(Collider other)
    {
        OnHit(other.gameObject.GetComponent<Damager>());
    }

    void OnCollisionEnter(Collision other)
    {
        OnHit(other.gameObject.GetComponent<Damager>());
    }

    protected virtual void OnHit(Damager damager) {
        if (damager == null) return;
        if (damagerMask.value != (damagerMask.value | (1 << damager.gameObject.layer))) return;

        Damage(damager.damage);
    }

    protected virtual void Damage(float amount) {
        if (Time.time < lastHitTime + invincibilityTime) return;
        health -= amount;
        lastHitTime = Time.time;
        if (animator) animator.SetTrigger("hurt");
        if (health < 0) { Die(); }
    }

    protected virtual void Die() {
        GetComponents<Collider>().ToList().ForEach(c => c.enabled = false);
        GetComponentsInChildren<Collider>().ToList().ForEach(c => c.enabled = false);
        if (animator) animator.SetTrigger("die");
        StartCoroutine(DieDelayed());
    }

    IEnumerator DieDelayed() {
        yield return new WaitForSeconds(dieDelay);
        Destroy(mainObject);
    }

}