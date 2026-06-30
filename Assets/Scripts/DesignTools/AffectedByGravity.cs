//Make sure Object has Rigidbody2D attached


using System.Collections;
using System.ComponentModel;
using UnityEngine;



public class AffectedByGravity : MonoBehaviour
{
    public bool inAir;
    [SerializeField] private float gravityMult = 1;
    [SerializeField] private float coyoteTime = 0.5f;


    void Start()
    {
        GameManager.Instance.AddGravityAffectedObj(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player") && !CheckpointManager.Instance.Dead)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * 0.5f + Vector3.down * 0.6f, Vector2.up, 1.2f);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * 0.5f + Vector3.down * 0.6f, Vector2.up, 1.2f);
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position + Vector3.left * 0.3f + Vector3.up * 0.45f, Vector3.right, 0.6f);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position + Vector3.left * 0.3f - Vector3.up * 0.45f, Vector3.right, 0.6f);


            Debug.DrawRay(transform.position + Vector3.left * 0.25f + Vector3.up * 0.5f, Vector3.right * 0.5f);

            if (hitUp && hitDown && hitUp.collider.CompareTag("Ground") && hitDown.collider.CompareTag("Ground"))
            {
                CheckpointManager.Instance.Respawn();
            }

            if ((hitLeft && hitLeft.collider.CompareTag("Ground")) || (hitRight && hitRight.collider.CompareTag("Ground")))
            {
                inAir = false;

                GameManager.Instance.ResetGravity();
                GetComponent<Animator>().SetBool("Falling", false);

            }

            else StartCoroutine(CoyoteTime());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        inAir = true;
        if (gameObject.CompareTag("Player"))
            GetComponent<Animator>().SetBool("Falling", true);
    }

    public float GetGravityMult()
    {
        return gravityMult;
    }

}
