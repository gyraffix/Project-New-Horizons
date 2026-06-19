using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DashRing : MonoBehaviour
{
    private enum DashDirection { Left, Right, Universal }    
    [SerializeField] private string checkForTag;
    [SerializeField] private DashDirection dashDirection = DashDirection.Left;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(checkForTag))
            return;
        bool dashRight = false;
        bool universalDash = false;
        switch (dashDirection)
        {
            case DashDirection.Left:
                dashRight = false;
                break;
            case DashDirection.Right:
                dashRight = true; 
                break;
            case DashDirection.Universal:
                universalDash = true;
                break;
        }
        PlayerControler.Instance.Dash(dashRight, universalDash);
    }
}
