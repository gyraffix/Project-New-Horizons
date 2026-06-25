using UnityEngine;

public class ParallaxBG : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxMult;
    private float cameraStartX;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraStartX = cameraTransform.position.x;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Vector3.right * ((cameraTransform.position.x - cameraStartX)/(1/parallaxMult));
    }
}
