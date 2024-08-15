using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private int dashEnergyCost = 50;
    [SerializeField] private float energyRechargeRate = 140f; // Năng lượng hồi mỗi giây

    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashTime = 0f;
    private int currentEnergy;

    private float energyRechargeTimer = 0f; // Biến theo dõi thời gian hồi

    public bool IsDashing => isDashing;

    private void Awake()
    {
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        if (isDashing)
        {
            DashMove();
        }
    }

    public void TryDash(Vector2 direction)
    {
        if (!isDashing && currentEnergy >= dashEnergyCost && direction != Vector2.zero)
        {
            StartDash(direction);
        }
    }

    private void StartDash(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashDirection = direction;
            UseEnergy(dashEnergyCost);
        }
    }

    private void DashMove()
    {
        if (dashTime > 0)
        {
            Vector3 dashMovement = new Vector3(dashDirection.x, dashDirection.y, 0);
            transform.position += dashMovement * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    public void RechargeEnergy(float deltaTime)
    {
        // Thời gian hồi năng lượng mỗi giây theo đúng tỷ lệ
        energyRechargeTimer += deltaTime;

        // Cứ mỗi giây sẽ hồi 140 năng lượng
        if (energyRechargeTimer >= 1f)
        {
            int energyToRecharge = Mathf.FloorToInt(energyRechargeRate * energyRechargeTimer); // Tính số năng lượng hồi lại
            currentEnergy += energyToRecharge;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy); // Đảm bảo không vượt quá năng lượng tối đa
            energyRechargeTimer = 0f; // Reset bộ đếm
        }
    }

    private void UseEnergy(int amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public int GetMaxEnergy()
    {
        return maxEnergy;
    }
}
