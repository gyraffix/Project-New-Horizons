using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    
    [SerializeField] private float magnitude;

    [Header("Gravity Variables")]
    [SerializeField] private float gravityCooldown;
    private bool coolDownActive;

    private float direction;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        NormalMovement();
        SwitchGravity();
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = direction * magnitude;
    }

    private void NormalMovement()
    {
        Vector2 directions = new Vector2(1, -1);

        direction = 0;
        if (Input.GetKey(moveRight))
        {
            direction = directions.x;
        }
        else if (Input.GetKey(moveLeft))
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
}
