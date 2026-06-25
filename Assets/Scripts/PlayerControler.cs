using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler : MonoBehaviour
{
    private static PlayerControler instance;

    [Header("Keybinds")]
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode respawn;
    [SerializeField] private KeyCode pause;

    [Header("Dash Variables")]
    [SerializeField] private float dashMagnitude;
    [SerializeField] private float dashDuration;
    private bool dashing;
    private Vector2 dashDirection;

    [Header("Gravity Variables")]
    [SerializeField] private float gravityCooldown;
    private bool coolDownActive;

    [Header("Movement variables")]
    [SerializeField] private float magnitude;
    private float direction;
    private bool movingLeft = false;
    private bool movingRight = false;

    private Rigidbody2D rb;

    [Header("Animation & Juice")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private ParticleSystem particleGravityUp;
    [SerializeField] private ParticleSystem particleGravityDown;




    #region Public getters
    public static PlayerControler Instance { get { return instance; } }
    public float Direction { get { return direction; } }

    #endregion

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        NormalMovement();
        SwitchGravity();
        Respawn();
        Pause();
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = direction * magnitude;
        if (direction == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else if (direction == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }

        StartCoroutine(FixedUpdateDash());
    }


    private void NormalMovement()
    {
        Vector2 directions = new Vector2(1, -1);

        direction = 0;
        if (Input.GetKey(moveRight) || movingRight)
        {
            direction = directions.x;
        }
        else if (Input.GetKey(moveLeft) || movingLeft)
        {
            direction = directions.y;
        }
    }

    private IEnumerator GravityCooldown()
    {
        coolDownActive = true;
        yield return new WaitForSeconds(gravityCooldown);
        coolDownActive = false;
    }

    private void SwitchGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !coolDownActive)
        {
            StartCoroutine(GravityCooldown());
            GameManager.Instance.SwapGravity();
        }
    }

    public void SwitchGravityTouch()
    {
        if (!coolDownActive)
        {
            StartCoroutine(GravityCooldown());
            GameManager.Instance.SwapGravity();
        }
    }

    public void RunParticles()
    {
        if (GameManager.Instance.GetGravity())
        {
            particleGravityUp.Play();
        }
        else
        {
           particleGravityDown.Play();
        }
    }

    public void Dash(bool dashRight, bool universalDash)
    {
        if (universalDash)
        {
            dashDirection = new Vector2(rb.linearVelocity.x, 0);
            dashDirection.Normalize();
        }
        else if (dashRight)
            dashDirection.x = 1;
        else dashDirection.x = -1;

        dashing = true;
    }

    float timer;

    private IEnumerator FixedUpdateDash()
    {
        if (dashing)
        {
            if (dashDirection.x != 0)
            {                
                rb.AddForce(dashDirection * dashMagnitude, ForceMode2D.Impulse);
                while (timer < dashDuration / 2.5f)
                {
                    rb.linearVelocityY = 0;
                    yield return new WaitForFixedUpdate();
                    timer += Time.fixedDeltaTime;
                }
                yield return new WaitForSeconds(dashDuration / 1.5f);

                dashing = false;            
            }
        }
        else timer = 0;
    }

    public void SwitchMovingLeft()
    {
        movingLeft = !movingLeft;
    }

    public void SwitchMovingRight()
    {
        movingRight = !movingRight;
    }

    private void Respawn()
    {
        if (Input.GetKeyDown(respawn))
            CheckpointManager.Instance.Respawn();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(pause))
            GameManager.Instance.Pause();
    }

    public void ResetMovement()
    {        
        dashing = false;
        direction = 0;

        rb.linearVelocity = new Vector2(0, 0);
    }
}
