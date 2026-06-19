using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RainManager : MonoBehaviour
{        
    [SerializeField] private GameObject gravDown;
    [SerializeField] private GameObject gravUp;
    [SerializeField] private GameObject hitboxes;

    [SerializeField] private ParticleSystem[] allRainDown;
    [SerializeField] private ParticleSystem[] allRainUp;

    private bool covered;    

    void Start()
    {
        
        gravDown = transform.Find("Grav Down").gameObject;
        gravUp = transform.Find("Grav Up").gameObject;
        hitboxes = transform.Find("Rain Hitboxes").gameObject;
              
        allRainDown = gravDown.transform.GetComponentsInChildren<ParticleSystem>(true);
        allRainUp = gravUp.transform.GetComponentsInChildren<ParticleSystem>(true);                
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

    private void OnDrawGizmos()
    {
        foreach (var partSystem in allRainDown)
        {
            Gizmos.DrawLine(new Vector2(partSystem.transform.position.x - (partSystem.shape.length / 2), partSystem.transform.position.y), 
                new Vector2(partSystem.transform.position.x + (partSystem.shape.length / 2), partSystem.transform.position.y));            
        }
        foreach (var partSystem in allRainUp)
        {
            Gizmos.DrawLine(new Vector2(partSystem.transform.position.x - (partSystem.shape.length / 2), partSystem.transform.position.y),
                new Vector2(partSystem.transform.position.x + (partSystem.shape.length / 2), partSystem.transform.position.y));            
        }        
    }
}
