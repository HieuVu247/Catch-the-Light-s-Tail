using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 5f; // Tốc độ tối đa khi di chuyển
    [SerializeField] private float acceleration = 10f; // Tốc độ tăng dần
    [SerializeField] private float deceleration = 5f; // Tốc độ giảm dần khi ngừng di chuyển
    [SerializeField] private float slideThreshold = 0.01f; // Ngưỡng tốc độ dừng hoàn toàn

    private Vector2 moveDirection;
    private float currentSpeed = 0f;
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
    }

    public void SetMoveDirection(Vector2 direction)
    {
        if (!playerDash.IsDashing) // Cập nhật hướng di chuyển khi không lướt
        {
            moveDirection = direction.normalized;
        }
    }

    private void Move()
    {
        if (moveDirection != Vector2.zero)
        {
            // Tăng tốc dần lên tốc độ tối đa
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxMoveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Giảm tốc dần về 0 khi không di chuyển
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);

            // Nếu tốc độ nhỏ hơn ngưỡng, dừng hoàn toàn
            if (currentSpeed <= slideThreshold)
            {
                currentSpeed = 0f;
            }
        }

        // Di chuyển nhân vật với tốc độ hiện tại
        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
