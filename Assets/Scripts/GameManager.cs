using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static string savePath = "Assets/Data.txt";
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();

    //Gravity Variables

    [SerializeField] private float gravityStrength = 9.81f;
    private bool gravityUp = false;

    #region Public getters
    public static GameManager Instance
    { get { return instance; } }

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

        if (Time.timeSinceLevelLoad < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " time"))
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " stars", Time.timeSinceLevelLoad);
    }


    public void SwapGravity()
    {
        gravityUp = !gravityUp;

        if (gravityUp)
        {
            foreach (var obj in gravityAffectedObjects)
            {
                obj.gravityScale = -gravityStrength;
            }
        }
        else
        {
            foreach (var obj in gravityAffectedObjects)
            {
                obj.gravityScale = gravityStrength;
            }
        }
    }

}
