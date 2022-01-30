using System.Collections;
using UnityEngine;

public class Bat : Damageable {

    public float farSpeed = 12f;
    public float speed = 5f;
    public float flyRecoil = 0.5f;
    public float flyForce = 1f;
    public float gravity = 4f;
    public Rigidbody rb;
    public float flyHeight = 2f;
    public float heightDelta = .4f;
    public float attackDistance = 1f;
    public float cooldown = 1f;


    bool underCooldown = false;
    Transform target;
    float heightOffset = 0f;
    Collider coll;

    void Start()
    {
        target = PlayerMotor.player;
        coll = rb.GetComponent<Collider>();
        StartCoroutine(DoFly());
    }

    void Update() {
        animator.SetBool("moving", !underCooldown);
    }

    void FixedUpdate()
    {
        // Aim to player
        Vector3 forward = target.position - rb.position;
        forward.y = 0;
        if (!underCooldown) {
            rb.MoveRotation(Quaternion.LookRotation(forward, rb.transform.up));
        }

        Vector3 force = Vector3.zero;
        Vector3 velocity = rb.velocity;
        // Flight Stabilization
        float heightDiff = (flyHeight + heightOffset) - rb.position.y;
        if (heightDiff < .04f && rb.velocity.sqrMagnitude > 1f) {
            velocity.y /= 2;
        }

        if (Mathf.Abs(heightDiff) > heightDelta)
            force.y += (heightDiff) * 2;

        // Virtual gravity
        force.y -= gravity;

        // Bats always move straight
        if (forward.sqrMagnitude < attackDistance * attackDistance) {
            Vector2 addSpeed = new Vector2(rb.transform.forward.x, rb.transform.forward.z) * speed * Time.deltaTime;
            addSpeed += new Vector2(velocity.x, velocity.z);
            addSpeed = addSpeed.normalized * speed;
            velocity.x = addSpeed.x;
            velocity.z = addSpeed.y;
        } else {
            Vector3 fwVelocity = rb.transform.forward * farSpeed;
            velocity.x = fwVelocity.x;
            velocity.z = fwVelocity.z;
        }

        // Evade obstacles
        Vector3 feet = rb.position;
        feet.y = coll.bounds.min.y;
        Physics.Raycast(feet, rb.transform.forward, out RaycastHit hit, .75f);
        if (hit.collider != null) {
            heightOffset += Time.deltaTime;
            animator.SetBool("shifting", true);
        } else if (heightOffset > 0) {
            heightOffset -= Time.deltaTime;
        } else {
            animator.SetBool("shifting", false);
            heightOffset = 0;
        }
        
        rb.velocity = velocity;
        rb.AddForce(force);
    }

    protected override void OnHit(Damager damager) {
        base.OnHit(damager);
        if (damager == null || underCooldown) return;

        if (damager.transform == target)
            StartCoroutine(DoCooldown());
    }

    IEnumerator DoCooldown() {
        underCooldown = true;
        yield return new WaitForSeconds(cooldown);
        underCooldown = false;
    }

    IEnumerator DoFly() {
        while (true) {
            rb.AddForce(transform.up * flyForce, ForceMode.Impulse);
            yield return new WaitForSeconds(flyRecoil);
        }
    }

}