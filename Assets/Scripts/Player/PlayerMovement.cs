using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection;

    private void Update()
    {
        Move();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction; // Nhận hướng di chuyển từ PlayerController
    }

    private void Move()
    {
        if (moveDirection != Vector2.zero)
        {
            Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0);
            transform.position += movement * moveSpeed * Time.deltaTime; // Di chuyển nhân vật
        }
    }
}
