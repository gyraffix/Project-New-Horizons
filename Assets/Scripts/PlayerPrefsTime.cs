using System;
using TMPro;
using UnityEngine;

public class PlayerPrefsTime : MonoBehaviour
{
    [SerializeField] private string playerPrefName;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        if (PlayerPrefs.GetFloat(playerPrefName) != 0)
        {
            text.text = "Best time: " + SectondsToString(PlayerPrefs.GetFloat(playerPrefName));
        }
        else
        {
            text.text = "Best time: -";
        }
    }

    public string SectondsToString(float timeSeconds)
    {
        int minutes = (int)(timeSeconds - timeSeconds % 60) / 60;
        int seconds = (int)(Math.Truncate(timeSeconds) - minutes * 60);

        string minutesText = minutes.ToString();
        string secondsText = seconds.ToString().Length == 1 ? "0" + seconds.ToString() : seconds.ToString();

        return minutesText + ":" + secondsText;
    }

}
