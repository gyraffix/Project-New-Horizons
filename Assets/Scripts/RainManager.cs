using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{        
    [SerializeField] private GameObject gravDown;
    [SerializeField] private GameObject gravUp;
    [SerializeField] private GameObject hitboxes;

    private bool covered;

    void Start()
    {
        
        gravDown = transform.Find("Grav Down").gameObject;
        gravUp = transform.Find("Grav Up").gameObject;
        hitboxes = transform.Find("Rain Hitboxes").gameObject;
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
