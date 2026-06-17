using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{    
    private static RainManager instance;
    [SerializeField] private Transform rain;
    [SerializeField] private GameObject gravDown;
    [SerializeField] private GameObject gravUp;
    [SerializeField] private GameObject hitboxes;

    private bool covered;

    #region Public getters
    public static RainManager Instance { get { return instance; } }
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

    void Start()
    {
        rain = GameObject.Find("Rain").transform;
        gravDown = rain.Find("Grav Down").gameObject;
        gravUp = rain.Find("Grav Up").gameObject;
        hitboxes = rain.Find("Rain Hitboxes").gameObject;
    }

    void Update()
    {        
        if (GameManager.Instance.GravityUp)
        {
            gravUp.SetActive(true);
            gravDown.SetActive(false);
        }
        else
        {
            gravUp.SetActive(false);
            gravDown.SetActive(true);
        }

        if (covered)        
            hitboxes.SetActive(false);        
        else
            hitboxes.SetActive(true);
    }

    public void SwitchCoverState()
    {
        covered = !covered;
    }
}
