using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager instance;
    private PlayerControler player;
    [SerializeField] private Transform currentCheckpoint;
    [SerializeField] private float animationDuration = 0.5f;
    private HashSet<Transform> previousCheckpoints = new();

    #region Public getters
    public static CheckpointManager Instance { get { return instance; } }

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        if (previousCheckpoints.Contains(checkpoint))
            return;

        PlayerControler.Instance.RunParticles();
        previousCheckpoints.Add(currentCheckpoint);
        currentCheckpoint = checkpoint;
        GetComponent<AudioSource>().Play();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        player.GetComponent<Animator>().SetTrigger("Dying");
        var playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        PlayerControler.Instance.DeathParticles();
        yield return new WaitForSeconds(animationDuration);
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;       

        if (GameManager.Instance.GravityUp)
            GameManager.Instance.SwapGravity();

        PlayerControler.Instance.ResetMovement();
        player.transform.position = currentCheckpoint.position;
        PlayerControler.Instance.RunParticles();
    }
}
