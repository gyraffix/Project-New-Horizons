using UnityEngine;
using UnityEngine.Events;

public class UnityEvent_onTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }
}
