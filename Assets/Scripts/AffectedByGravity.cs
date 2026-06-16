//Make sure Object has Rigidbody2D attached


using System.ComponentModel;
using UnityEngine;



public class AffectedByGravity : MonoBehaviour
{


    void Start()
    {
        GameManager.Instance.AddGravityAffectedObj(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
