using System;
using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private static PlayerControler instance;

    [Header("Keybinds")]
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode respawn;

    [SerializeField] private float magnitude;

    [Header("Dash Variables")]
    [SerializeField] private float dashMagnitude;
    [SerializeField] private float dashDuration;
    private bool dashing;
    private bool horizontalOnlyDash;
    private Vector2 dashDirection;

    [Header("Gravity Variables")]
    [SerializeField] private float gravityCooldown;
    private bool coolDownActive;

    private float direction;

    private bool movingLeft = false;
    private bool movingRight = false;

    private Rigidbody2D rb;

    #region Public getters
    public static PlayerControler Instance { get { return instance; } }
    public float Direction { get { return direction; } }
    public float Magnitude { get { return magnitude; } }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        NormalMovement();
        SwitchGravity();
        Respawn();
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = direction * magnitude;
        StartCoroutine(FixedUpdateDash());
    }

    private IEnumerator FixedUpdateDash()
    {
        if (dashing)
        {
            rb.AddForce(dashDirection * dashMagnitude, ForceMode2D.Impulse);
            if (horizontalOnlyDash)
                rb.linearVelocityY = 0;
            yield return new WaitForSeconds(dashDuration);
            dashing = false;
        }
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

    public void Dash(bool dashRight, bool universalDash, bool horizontalOnly)
    {
        if (universalDash)
        {
            dashDirection = new Vector2(rb.linearVelocity.x, 0);
            dashDirection.Normalize();
        }
        else if (dashRight)
            dashDirection.x = 1;
        else dashDirection.x = -1;

        horizontalOnlyDash = horizontalOnly;
        dashing = true;
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
        {
            CheckpointManager.Instance.Respawn();
        }
    }
}
