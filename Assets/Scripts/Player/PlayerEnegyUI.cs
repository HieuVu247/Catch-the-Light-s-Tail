using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyUI : MonoBehaviour
{
    [SerializeField] private Slider energySlider; // Tham chiếu tới Slider UI
    private PlayerDash playerDash;

    private void Awake()
    {
        playerDash = FindObjectOfType<PlayerDash>(); // Lấy tham chiếu tới PlayerDash
        if (playerDash != null && energySlider != null)
        {
            energySlider.maxValue = playerDash.GetMaxEnergy(); // Thiết lập max value của Slider
            energySlider.value = playerDash.GetCurrentEnergy(); // Thiết lập giá trị ban đầu
        }
    }

    private void Update()
    {
        if (playerDash != null)
        {
            energySlider.value = playerDash.GetCurrentEnergy(); // Cập nhật giá trị của Slider theo năng lượng hiện tại
        }
    }
}
