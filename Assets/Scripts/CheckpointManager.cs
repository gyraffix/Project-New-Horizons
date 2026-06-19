using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager instance;
    private PlayerControler player;
    [SerializeField] private Transform currentCheckpoint;
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

        previousCheckpoints.Add(currentCheckpoint);
        currentCheckpoint = checkpoint;
    }

    public void Respawn(float animDuration)
    {
        StartCoroutine(RespawnDelay(animDuration));
    }

    private IEnumerator RespawnDelay(float animDuration)
    {

        yield return new WaitForSeconds(animDuration);

        if (GameManager.Instance.GravityUp)
            GameManager.Instance.SwapGravity();

        PlayerControler.Instance.ResetMovement();
        player.transform.position = currentCheckpoint.position;
    }
}
