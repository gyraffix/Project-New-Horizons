using DG.Tweening;
using System.Collections;
using Unity.Android.Gradle.Manifest;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField, Range(0.0001f, 0.05f)] private float cameraSpeed;
    [SerializeField] private float maxDistance;
    private Vector3 defaultPosition;
    private float cameraDestination;

    private void Start()
    {
        Instance = this;
        defaultPosition = transform.position;

    }


    private void Update()
    {
        cameraDestination = PlayerControler.Instance.Direction * maxDistance - transform.localPosition.x;
        transform.Translate(new Vector3(cameraDestination * cameraSpeed, 0, 0));


        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                transform.parent.transform.position.x - maxDistance,
                transform.parent.transform.position.x + maxDistance),
            defaultPosition.y,
            defaultPosition.z);
    }
}
