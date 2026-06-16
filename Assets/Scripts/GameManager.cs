using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();
    [SerializeField] private Level[] levels;


    [Header("Gravity Variables")]
    [SerializeField] private float gravityStrength = 1f;
    [SerializeField] private float initialSwapVelocity = 1f;
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

        instance = this;

        DontDestroyOnLoad(gameObject);
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

    public void LevelComplete(int levelNumber, int numberOfStars)
    {
        if (numberOfStars > levels[levelNumber].maxStarts)
            levels[levelNumber].maxStarts = numberOfStars;
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

}
public class Level
{
    public string name;
    public int maxStarts;
}
