using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static SceneSwitcher instance;
    
    [SerializeField] private KeyCode nextSceneKey;
    [SerializeField] private KeyCode prevSceneKey;

    #region Public getters
    public static SceneSwitcher Instance { get { return instance; } }    
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;        
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
            GameManager.Instance.SetGravityDown();
            NextScene();
        }
        if (Input.GetKeyDown(prevSceneKey))
        {
            GameManager.Instance.SetGravityDown();
            PreviousScene();
        }
    }

    public void SwitchScene(int buildIndex)
    {
        GameManager.Instance.SetGravityDown();
        SceneManager.LoadScene(buildIndex);
    }

    
    public void ReloadScene()
    {
        GameManager.Instance.SetGravityDown();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }

    public void NextScene()
    {
        GameManager.Instance.SetGravityDown();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex + 1);
    }

    public void PreviousScene()
    {
        GameManager.Instance.SetGravityDown();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex - 1);
    }
}
