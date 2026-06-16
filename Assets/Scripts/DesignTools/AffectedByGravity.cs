//Make sure Object has Rigidbody2D attached


using System.ComponentModel;
using UnityEngine;



public class AffectedByGravity : MonoBehaviour
{
    public bool inAir;

    void Start()
    {
        GameManager.Instance.AddGravityAffectedObj(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 0.6f, Vector2.up, 1.2f);
        
        Debug.DrawLine(transform.position + Vector3.up * 0.6f, transform.position + Vector3.down * 0.6f, color: Color.blue);
        if (hit && !hit.collider.CompareTag("Player")) inAir = false;
        else inAir = true;
        
    }

    
}
