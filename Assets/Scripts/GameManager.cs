using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;


    [Header("Gravity Variables")]
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();
    [SerializeField] private float gravityStrength = 1f;
    [SerializeField] private float initialSwapVelocity = 1f;
    [SerializeField] private int totalGravitySwitches = 1;
    private int currentGravitySwitches = 1;
    private bool gravityUp = false;

    [SerializeField] private int flamesCollected;

    [Header("Sound")]
    [SerializeField] private AudioClip levelCompleteSFX;
    [SerializeField] private AudioClip gravSwitchSFX;
    [SerializeField] private AudioClip flameSFX;
    [SerializeField] private AudioClip musicStart;
    [SerializeField] private AudioSource generalSounds;
    [SerializeField] private AudioSource musicLoop;



    [Header("Player Variables")]
    [SerializeField] Color hasGravityColor;
    [SerializeField] Color noGravityColor;
    [HideInInspector] public bool hasSeenIntro = false;

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

    private void Start()
    {
        StartCoroutine(StartMusic());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gravityAffectedObjects = new();
        Time.timeScale = 1;
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

        generalSounds.PlayOneShot(levelCompleteSFX, 0.2f);

        flamesCollected = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Backspace))
        {
            ResetPlayerPrefs();
        }
    }
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public void AddFlame()
    {
        generalSounds.PlayOneShot(flameSFX, 0.6f);
        flamesCollected++;
    }
    public bool GetGravity()
    {
        return gravityUp;
    }

    public void ForceSwapGravity()
    {
        if (PlayerControler.Instance.GetComponent<AffectedByGravity>().inAir)
        {
            currentGravitySwitches--;
            PlayerControler.Instance.ChangeIndicatorColor(noGravityColor);
        }
        gravityUp = !gravityUp;
        float gravity;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipY = !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipY;

        if (gravityUp)
        {
            gravity = -gravityStrength;
            generalSounds.pitch = 1.03f;
        }
        else
        {
            gravity = gravityStrength;
            generalSounds.pitch = 0.97f;
        }
        generalSounds.volume = 0.4f;
        generalSounds.clip = gravSwitchSFX;
        generalSounds.Play();

        foreach (var obj in gravityAffectedObjects)
        {
            if (!obj.GetComponent<AffectedByGravity>().inAir)
                obj.linearVelocityY = initialSwapVelocity * -gravity;
            else obj.linearVelocityY = obj.linearVelocityY / 4;
            obj.gravityScale = gravity * obj.GetComponent<AffectedByGravity>().GetGravityMult();
        }

        PlayerControler.Instance.RunParticles();
    }
    private IEnumerator StartMusic()
    {
        generalSounds.PlayOneShot(musicStart, 0.2f);
        yield return new WaitForSeconds(musicStart.length);
        musicLoop.Play();
    }

    public void SwapGravity()
    {
        if (currentGravitySwitches <= 0 || Time.timeScale < 0.5f)
            return;

        ForceSwapGravity();
    }

    public void ResetGravity()
    {
        currentGravitySwitches = totalGravitySwitches;
        PlayerControler.Instance.ChangeIndicatorColor(hasGravityColor);
    }

    public void SetGravityDown()
    {
        if (GravityUp)
            SwapGravity();
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
    public void ToggleIntro()
    {
        hasSeenIntro = true;
    }
}
