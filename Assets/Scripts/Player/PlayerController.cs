using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerDash))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;

    private void Awake()
    {
        controls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Dash.performed += OnDash;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Dash.performed -= OnDash;
        controls.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        playerMovement.SetMoveDirection(moveInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        playerMovement.SetMoveDirection(moveInput);
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        playerDash.TryDash(moveInput);
    }
}
