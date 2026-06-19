using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPrefsSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        slider.value = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " stars");
    }
}
