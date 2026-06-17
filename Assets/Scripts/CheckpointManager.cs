using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager instance;
    private PlayerControler player;
    [SerializeField] private Transform currentCheckpoint;
    private HashSet<Transform> previousCheckpoints;

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
        if (previousCheckpoints.Contains(transform))
            return;

        previousCheckpoints.Add(currentCheckpoint);
        currentCheckpoint = transform;
    }

    public void Respawn()
    {
        if (GameManager.Instance.GravityUp)
            GameManager.Instance.SwapGravity();

        player.transform.position = currentCheckpoint.position;
    }
}
