using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();


    [Header("Gravity Variables")]
    [SerializeField] private float gravityStrength = 1f;
    [SerializeField] private float initialSwapVelocity = 1f;
    private bool gravityUp = false;

    #region Public getters
    public static GameManager Instance { get { return instance; } }
    public bool GravityUp { get { return gravityUp; } }

    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gravityAffectedObjects = new();
    }

    public void AddGravityAffectedObj(Rigidbody2D gameObject)
    {
        gravityAffectedObjects.Add(gameObject);
    }

    public void AddGravityAffectedObj(List<Rigidbody2D> gameObjects)
    {
        foreach (Rigidbody2D gameObject in gameObjects)
        {
            gravityAffectedObjects.Add(gameObject);
        }
    }

    public void LevelComplete(int numberOfStars)
    {
        if (numberOfStars > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " stars"))
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " stars", numberOfStars);

        if (Time.timeSinceLevelLoad < (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " is completed") == 0 ? Mathf.Infinity : PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " time")))
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " time", Time.timeSinceLevelLoad);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " is completed", 1);
    }


    public void SwapGravity()
    {
        gravityUp = !gravityUp;
        float gravity;

        if (gravityUp)
        {
            gravity = -gravityStrength;
        }
        else
        {
            gravity = gravityStrength;
        }

        foreach (var obj in gravityAffectedObjects)
        {
            if (!obj.GetComponent<AffectedByGravity>().inAir)
                obj.linearVelocityY = initialSwapVelocity * -gravity;
            obj.gravityScale = gravity;
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
