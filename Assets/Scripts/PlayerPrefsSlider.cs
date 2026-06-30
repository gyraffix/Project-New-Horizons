using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPrefsSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int sceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)   
            UpdateMenuSlider();
        else
            UpdateSlider();

    }

    public void UpdateSlider()
    {
        slider.value = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " stars");
    }

    public void UpdateMenuSlider()
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        slider.value = PlayerPrefs.GetInt(sceneName + " stars");        
    }
}
