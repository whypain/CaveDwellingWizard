using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] float minWalkingSpd = 0.1f; 

    [Header("References")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    private bool hasMvmentInput;
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
        if (hasMvmentInput)
        {
            Move();
        }

        animator.SetBool("Walking", Mathf.Abs(rb.linearVelocityX) > minWalkingSpd);
    }


    private void Move()
    {
        Vector2 velocity = mvmentInput * speed * Time.deltaTime;
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

    }
}
