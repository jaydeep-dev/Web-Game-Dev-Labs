using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private bool cameraRelative = true;
    [SerializeField] private CinemachineVirtualCameraBase playerCam;
    private Transform camTransform;

    [Header("Jump")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float gravityScale = -30f;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 velocity;

    private CharacterController controller;
    private PlayerInputController inputController;

    #endregion

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        inputController.OnJumpPerformed += OnJump;
    }

    private void OnDisable()
    {
        inputController.OnJumpPerformed -= OnJump;
    }

    private void OnJump()
    {
        if (isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityScale);
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleGravity();
    }

    private void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravityScale * Time.fixedDeltaTime;

        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void HandleMovement()
    {
        Vector2 move = inputController.MoveInput;
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
