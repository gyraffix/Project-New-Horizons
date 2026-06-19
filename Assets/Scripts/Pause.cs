using UnityEngine;

public class Pause : MonoBehaviour
{
    public void ReferencePause()
    {
        GameManager.Instance.Pause();
    }
}
