using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private int dashEnergyCost = 50;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private float energyRechargeRate = 10f; // 10 năng lượng mỗi giây

    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashTime = 0f;

    private int currentEnergy;
    private float energyRechargeTimer = 0f;

    public bool IsDashing => isDashing;
    public bool CanDash => currentEnergy >= dashEnergyCost;

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
        else
        {
            RechargeEnergy(Time.deltaTime);
        }
    }

    public void TryDash(Vector2 direction)
    {
        if (!isDashing && CanDash && direction != Vector2.zero)
        {
            StartDash(direction);
        }
    }

    private void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashTime = dashDuration;
        dashDirection = direction;
        UseEnergy(dashEnergyCost);
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

    private void RechargeEnergy(float deltaTime)
    {
        energyRechargeTimer += deltaTime;
        if (energyRechargeTimer >= 1f)
        {
            currentEnergy += Mathf.FloorToInt(energyRechargeRate);
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            energyRechargeTimer = 0f;
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
    