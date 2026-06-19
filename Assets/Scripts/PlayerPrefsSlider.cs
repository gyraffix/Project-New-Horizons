using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsSlider : MonoBehaviour
{
    [SerializeField] private string playerPrefName;
    [SerializeField] private Slider slider;

    private void Start()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        slider.value = PlayerPrefs.GetInt(playerPrefName);
    }
}
