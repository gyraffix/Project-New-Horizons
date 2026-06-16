using UnityEngine;
using UnityEngine.Events;

public class UnityEvent_onTrigger : MonoBehaviour
{
    [SerializeField] private string checkForTag;

    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (checkForTag == null || other.CompareTag(checkForTag))
            onTriggerEnter.Invoke();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (checkForTag == null || other.CompareTag(checkForTag))
            onTriggerStay.Invoke();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (checkForTag == null || other.CompareTag(checkForTag))
            onTriggerExit.Invoke();
    }
}
