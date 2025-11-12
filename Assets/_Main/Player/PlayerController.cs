using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] float jumpForce;
    [SerializeField] float falingGravity;
    [SerializeField] float fallingThreshold;
    [SerializeField] float speed;
    [SerializeField] float minWalkingSpd = 0.1f;
    [SerializeField] float groundCheckDist = 0.1f;
    [SerializeField] LayerMask whatIsGround;

    [Header("References")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    private bool hasMvmentInput;
    private bool isGrounded => CheckGrounded();
    private bool isGroundedLastFrame;
    private Vector2 mvmentInput;

    private void OnEnable()
    {
        InputSystem.actions["Player/Move"].performed += OnMove;
        InputSystem.actions["Player/Move"].canceled += UnMove;

        InputSystem.actions["Player/Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        InputSystem.actions["Player/Move"].performed -= OnMove;
        InputSystem.actions["Player/Move"].canceled -= UnMove;

        InputSystem.actions["Player/Jump"].performed -= OnJump;
    }

    private void Update()
    {
        animator.SetBool("Walking", Mathf.Abs(rb.linearVelocityX) > minWalkingSpd);

        if (!isGroundedLastFrame && isGrounded) OnLanded();
    }

    private void LateUpdate()
    {
        isGroundedLastFrame = isGrounded;
    }

    private void FixedUpdate()
    {
        if (hasMvmentInput)
        {
            Move();
        }

        if (rb.linearVelocityY < 0 && rb.linearVelocityY < fallingThreshold && !isGrounded)
        {
            rb.gravityScale = falingGravity;
        }
    }


    private void Move()
    {
        Vector2 velocity = mvmentInput * speed * Time.fixedDeltaTime;
        rb.linearVelocityX = velocity.x;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        mvmentInput = ctx.ReadValue<Vector2>();
        hasMvmentInput = true;

        if (mvmentInput.x < 0) playerTransform.localScale = new Vector3(-1, 1, 1);
        else playerTransform.localScale = Vector3.one;
    }

    private void UnMove(InputAction.CallbackContext ctx)
    {
        mvmentInput = Vector2.zero;
        hasMvmentInput = false;
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        if (!isGrounded) return;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void OnLanded()
    {
        rb.gravityScale = 1;
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, groundCheckDist, whatIsGround);
        return hit;
    }


    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(playerTransform.position, Vector2.down * groundCheckDist, Color.beige);
    }
}
