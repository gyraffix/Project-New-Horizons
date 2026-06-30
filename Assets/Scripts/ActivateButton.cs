using UnityEngine;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{
    private Button button;
    [SerializeField] bool automatic;

    private void Start()
    {
        button = GetComponent<Button>();
        if (automatic && GameManager.Instance.hasSeenIntro)
        {
            Press();
        }
    }



    public void Press()
    {
        button.onClick.Invoke();
    }
}
