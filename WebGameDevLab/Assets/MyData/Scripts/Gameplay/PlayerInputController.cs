using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    private PlayerInputActions inputActions;
    private HealthManager healthManager;

    public event System.Action OnJumpPerformed;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        healthManager = GetComponent<HealthManager>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => OnJumpPerformed?.Invoke();
        healthManager.OnDie += OnDie;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        healthManager.OnDie -= OnDie;
    }

    public void OnTouch_Jump(InputAction.CallbackContext context)
    {
        OnJumpPerformed?.Invoke();
    }

    public void OnTouch_Look(InputAction.CallbackContext context)
    {

    }

    public void OnTouch_Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnDie()
    {
        enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
