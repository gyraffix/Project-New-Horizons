using UnityEngine;
using UnityEngine.Events;

public class OnTouch : MonoBehaviour
{
    [SerializeField] private UnityEvent onTouch; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                
                RaycastHit hit; 
                Physics.Raycast(ray, out hit, 1000);
                    

                if (hit.collider != null)// && hit.collider.gameObject.Equals(gameObject))
                {
                    Debug.Log(hit.collider.name);
                    onTouch.Invoke();
                }
            }

            
        }
    }
}
