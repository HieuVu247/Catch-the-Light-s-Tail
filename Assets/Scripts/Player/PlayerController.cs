using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        controls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Dash.performed += OnDash; // Khi nhấn phím Dash
        controls.Player.Dash.canceled += OnDashCanceled; // Khi nhả phím Dash
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Dash.performed -= OnDash;
        controls.Player.Dash.canceled -= OnDashCanceled;
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
        // Nhấn giữ để lướt khi đủ năng lượng
        playerMovement.TryDash();
    }

    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        // Hủy bỏ khi nhả phím
    }
}
