using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();


    [Header("Gravity Variables")]
    [SerializeField] private float gravityStrength = 1f;
    [SerializeField] private float initialSwapVelocity = 1f;
    [SerializeField] private int totalGravitySwitches = 1;
    private int currentGravitySwitches = 1;
    private bool gravityUp = false;

    [SerializeField]private int flamesCollected;

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

    public void LevelComplete()
    {
        LevelComplete(flamesCollected);
    }

    public void LevelComplete(int numberOfStars)
    {
        if (numberOfStars > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " stars"))
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " stars", numberOfStars);

        if (Time.timeSinceLevelLoad < (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " is completed") == 0 ? Mathf.Infinity : PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " time")))
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " time", Time.timeSinceLevelLoad);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " is completed", 1);

        flamesCollected = 0;
    }

    public void AddFlame()
    {
        flamesCollected++;
    }
    public bool GetGravity()
    {
        return gravityUp;
    }

    public void SwapGravity()
    {
        if (currentGravitySwitches <= 0)
            return;

        if (PlayerControler.Instance.GetComponent<AffectedByGravity>().inAir)
            currentGravitySwitches--;
        gravityUp = !gravityUp;
        float gravity;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipY = !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipY;

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
            else obj.linearVelocityY = obj.linearVelocityY/4;
            obj.gravityScale = gravity * obj.GetComponent<AffectedByGravity>().GetGravityMult();
        }

        PlayerControler.Instance.RunParticles();
    }

    public void ResetGravity()
    {
        currentGravitySwitches = totalGravitySwitches;
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
