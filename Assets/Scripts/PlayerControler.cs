using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    
    [SerializeField] private float magnitude;

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

    private void NormalMovement()
    {
        Vector2 directions = new Vector2(1, -1);
        float direction = 0;
        if (Input.GetKey(moveRight))
        {
            direction = directions.x;
        }
        else if (Input.GetKey(moveLeft))
        {
            direction = directions.y;
        }


        rb.AddForce(new Vector2(magnitude, 0) * direction, ForceMode2D.Force);
    }
    private void SwitchGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.SwapGravity();
        }
    }
}
