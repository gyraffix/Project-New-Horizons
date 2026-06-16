using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private List<Rigidbody2D> gravityAffectedObjects = new();
    [SerializeField] private Level[] levels;

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
public class Level
{
    public string name;
    public int maxStarts;
}
