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
        controls = new PlayerControls(); // Sử dụng input action asset đã tạo
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Enable(); // Bật input
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Disable(); // Tắt input
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Nhận input từ người chơi
        playerMovement.SetMoveDirection(moveInput); // Gửi dữ liệu cho PlayerMovement
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        playerMovement.SetMoveDirection(moveInput);
    }
}
