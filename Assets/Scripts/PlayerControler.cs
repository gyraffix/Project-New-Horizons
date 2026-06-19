using System.Collections;
using System.Threading;
using UnityEngine;

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

    [Header("Animation")]
    [SerializeField] private float animationDuration = 0.5f;

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
            float storeVeloY = rb.linearVelocityY;
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
            CheckpointManager.Instance.Respawn(animationDuration);
    }

    private void Pause()
    {
        if (Input.GetKeyDown(pause))
            GameManager.Instance.Pause();
    }
}
