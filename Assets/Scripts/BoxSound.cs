using UnityEngine;

public class Box : MonoBehaviour
{

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (transform.localScale.y + 0.2f) / 2 * (GameManager.Instance.GravityUp ? Vector2.up : Vector2.down));

        if (hit && hit.transform.CompareTag("Ground"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
