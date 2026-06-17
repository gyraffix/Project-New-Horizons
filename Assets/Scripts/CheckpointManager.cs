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
        Debug.Log("set checkpoint");
        if (previousCheckpoints.Contains(checkpoint))
            return;

        previousCheckpoints.Add(currentCheckpoint);
        currentCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        if (GameManager.Instance.GravityUp)
            GameManager.Instance.SwapGravity();

        player.transform.position = currentCheckpoint.position;
    }
}
