using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckpointManager.Instance.Respawn();
            GetComponent<AudioSource>().Play();
        }
    }
}
