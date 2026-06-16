using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    
    [SerializeField] private KeyCode nextSceneKey;
    [SerializeField] private KeyCode prevSceneKey;

    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        ManualSwitch();
    }

    private void ManualSwitch()
    {
        if (Input.GetKeyDown(nextSceneKey))
        {
            Debug.Log("Pressed");
            NextScene();
        }
        if (Input.GetKeyDown(prevSceneKey))
        {
            PreviousScene();
        }
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextScene()
    {
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex + 1);
    }

    public void PreviousScene()
    {
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex - 1);
    }
}
