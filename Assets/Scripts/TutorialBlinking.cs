using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBlinking : MonoBehaviour
{
    [SerializeField] private RawImage blinker;
    [SerializeField] private float blinkSpeed;

    private bool tutorial = true;

    private void Update()
    {
        if (!tutorial)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }    

    public void EndTutorial()
    {
        tutorial = false;
    }
}
