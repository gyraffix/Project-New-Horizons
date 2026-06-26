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
        StartCoroutine(SwapGravityTutorial());
    }

    private IEnumerator SwapGravityTutorial()
    {
        if (tutorial)
        {
            Color currentColor = blinker.color;
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1) / 6;

            currentColor.a = alpha;
            blinker.color = currentColor;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            Color currentColor = blinker.color;
            currentColor.a = 0;
            blinker.color = currentColor;
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void EndTutorial()
    {
        tutorial = false;
    }
}
