using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position - offset;
    }
}
