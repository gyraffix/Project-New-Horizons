//Make sure Object has Rigidbody2D attached


using System.Collections;
using System.ComponentModel;
using UnityEngine;



public class AffectedByGravity : MonoBehaviour
{
    public bool inAir;
    [SerializeField] private float gravityMult = 1;
    [SerializeField] private float hitboxCheckLength = 0.5f;
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
            
            RaycastHit2D hitUpL = Physics2D.Raycast(transform.position + Vector3.left * 0.3f, Vector3.up, hitboxCheckLength);
            RaycastHit2D hitUpR = Physics2D.Raycast(transform.position + Vector3.right * 0.3f, Vector3.up, hitboxCheckLength);
            RaycastHit2D hitDownL = Physics2D.Raycast(transform.position + Vector3.left * 0.3f, Vector3.down, hitboxCheckLength);
            RaycastHit2D hitDownR = Physics2D.Raycast(transform.position + Vector3.right * 0.3f, Vector3.down, hitboxCheckLength);


            Debug.DrawRay(transform.position + Vector3.left * 0.3f, Vector3.up * 0.4f);
            Debug.DrawRay(transform.position + Vector3.right * 0.3f, Vector3.up * 0.4f);

            if ((hitUpL && hitUpL.collider.CompareTag("Ground") || (hitUpR && hitUpR.collider.CompareTag("Ground"))) && (hitDownL && hitDownL.collider.CompareTag("Ground") || (hitDownR && hitDownR.collider.CompareTag("Ground"))))
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
