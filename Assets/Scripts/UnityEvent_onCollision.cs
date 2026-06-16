using UnityEngine;
using UnityEngine.Events;

public class UnityEvent_onCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollisionEnter;
    [SerializeField] private UnityEvent onCollisionStay;
    [SerializeField] private UnityEvent onCollisionExit;

    private void OnCollisionEnter(Collision other)
    {
        onCollisionEnter.Invoke();
    }
    private void OnCollisionStay(Collision other)
    {
        onCollisionStay.Invoke();
    }
    private void OnCollisionExit(Collision other)
    {
        onCollisionExit.Invoke();
    }
}
