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
            CallGravityDown();
            CallUnpause();
            NextScene();
        }
        if (Input.GetKeyDown(prevSceneKey))
        {
            CallGravityDown();
            CallUnpause();
            PreviousScene();
        }
    }

    public void SwitchScene(int buildIndex)
    {
        CallGravityDown();
        CallUnpause();
        SceneManager.LoadScene(buildIndex);
    }

    
    public void ReloadScene()
    {
        CallGravityDown();
        CallUnpause();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }

    public void NextScene()
    {
        CallGravityDown();
        CallUnpause();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex + 1);
    }

    public void PreviousScene()
    {
        CallGravityDown();
        CallUnpause();
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex - 1);
    }

    private void CallGravityDown()
    {
        GameManager.Instance.SetGravityDown();
    }

    private void CallUnpause()
    {
        GameManager.Instance.Unpause();
    }
}
