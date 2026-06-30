using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsTime : MonoBehaviour
{
    [SerializeField] private string playerPrefName;
    [SerializeField] private TMP_Text text;

    [SerializeField] private enum TimeType { CurrentTime, BestTime };
    [SerializeField] private TimeType timeType;
    private void Start()
    {
        if (timeType == TimeType.CurrentTime)
            UpdateCurretnTimeSlider();
        else
            UpdateBestTimeSlider();
    }

    public void UpdateBestTimeSlider()
    {
        if (playerPrefName == "")
        {
            playerPrefName = SceneManager.GetActiveScene().name;            
        }
        if (PlayerPrefs.GetFloat(playerPrefName + " time")  != 0)
        {
            Debug.Log(PlayerPrefs.GetFloat(playerPrefName + " time"));
            text.text = "Best time: " + SectondsToString(PlayerPrefs.GetFloat(playerPrefName + " time"));
        }
        else
        {
            text.text = "Best time: 0:00";
        }
    }

    public void UpdateCurretnTimeSlider()
    {
        text.text = "Current time: " + SectondsToString(Time.timeSinceLevelLoad);
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
