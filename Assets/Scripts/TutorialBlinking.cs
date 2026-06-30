using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBlinking : MonoBehaviour
{
    [SerializeField] private Image blinker;

    private bool tutorial = true;

    private void Start()
    {
        if (GameManager.Instance.hasSeenIntro)
        {
            EndTutorial();
        }
    }

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
