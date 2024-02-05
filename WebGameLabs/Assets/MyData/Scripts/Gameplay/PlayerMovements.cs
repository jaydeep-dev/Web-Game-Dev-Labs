using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private bool cameraRelative = true;
    private Transform camTransform;
    private Vector2 move;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float gravityScale = -30f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private Vector2 velocity;

    private CharacterController controller;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => move = Vector2.zero;
        inputActions.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityScale);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleGravity();
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravityScale * Time.fixedDeltaTime;

        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void HandleMovement()
    {
        Vector3 movement;

        if (cameraRelative)
            movement = camTransform.forward * move.y + camTransform.right * move.x;
        else
            movement = new Vector3(move.x, 0, move.y);

        movement.y = 0;
        movement.Normalize();

        controller.Move(Time.fixedDeltaTime * speed * movement);

        if (movement != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.fixedDeltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
