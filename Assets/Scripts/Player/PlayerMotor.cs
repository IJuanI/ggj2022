using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMotor : MonoBehaviour {

    public static Transform player;
    public float groundDistance = .2f;
    public float groundOffset = .02f;
    public float speed = 3f;
    public float jumpForce = 10f;
    public Animator playerAnimator;


    int jumpCount = 0;
    bool grounded = false, jumping = false;
    Rigidbody rb;
    Collider coll;
    SpriteRenderer rend;
    Vector2 movement;

    void Awake() {
        player = transform;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        rb.velocity = new Vector3(movement.x, 0, movement.y) * speed + new Vector3(0, rb.velocity.y, 0);
        
        playerAnimator.SetBool("moving", movement.magnitude > 0);

        if (Mathf.Abs(movement.x) > 0.01f)
            rend.flipX = movement.x > 0;

        var feet = new Vector3(transform.position.x, coll.bounds.min.y + groundOffset, transform.position.z);
        Physics.Raycast(feet, Vector3.down, out RaycastHit hit, groundDistance);
        grounded = hit.collider != null;
        playerAnimator.SetBool("grounded", grounded);
        if (grounded) jumpCount = 0;
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        if (!ctx.performed) {
            if (ctx.canceled)
                movement = Vector2.zero;
            return;
        }

        movement = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        if (jumping || jumpCount > 0) return;
        jumping = true;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimator.SetTrigger("jump");
        ++jumpCount;
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown() {
        yield return new WaitForSeconds(.2f);
        jumping = false;
    }
}