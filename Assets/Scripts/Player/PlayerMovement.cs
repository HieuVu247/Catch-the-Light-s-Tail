using UnityEngine;

[RequireComponent(typeof(PlayerDash))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection;
    private PlayerDash playerDash;

    private void Awake()
    {
        playerDash = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        if (!playerDash.IsDashing) // Chỉ di chuyển khi không lướt
        {
            Move();
        }

        // Nạp năng lượng khi di chuyển
        if (moveDirection != Vector2.zero)
        {
            playerDash.RechargeEnergy(Time.deltaTime);
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;

        if (!playerDash.IsDashing) // Cập nhật hướng khi không lướt
        {
            Move();
        }
    }

    public void TryDash()
    {
        playerDash.TryDash(moveDirection); // Gọi Dash từ PlayerDash khi nhấn giữ phím
    }

    private void Move()
    {
        if (moveDirection != Vector2.zero)
        {
            Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0);
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }
}
