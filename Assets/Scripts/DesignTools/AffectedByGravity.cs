//Make sure Object has Rigidbody2D attached


using System.Collections;
using System.ComponentModel;
using UnityEngine;



public class AffectedByGravity : MonoBehaviour
{
    public bool inAir;
    [SerializeField] private float coyoteTime = 0.5f;

    void Start()
    {
        GameManager.Instance.AddGravityAffectedObj(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * 0.5f + Vector3.down * 0.6f, Vector2.up, 1.2f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * 0.5f + Vector3.down * 0.6f, Vector2.up, 1.2f);

        Debug.DrawLine(transform.position + Vector3.left * 0.5f + Vector3.down * 0.7f, transform.position + Vector3.down * 0.6f, color: Color.blue);
        if ((hitLeft && hitLeft.collider.CompareTag("Ground")) || (hitRight && hitRight.collider.CompareTag("Ground")))
        {
            inAir = false;
            if (gameObject.CompareTag("Player"))
            {
                GameManager.Instance.ResetGravity();
                GetComponent<Animator>().SetBool("Falling", false);
            }
        }
        else StartCoroutine(CoyoteTime());

    }

    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        inAir = true;
        if (gameObject.CompareTag("Player"))
            GetComponent<Animator>().SetBool("Falling", true);
    }

}
