using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioMixer masterMixer;

    public void SetMasterVolume(float soundValue)
    {
        masterMixer.SetFloat("MasterVol", soundValue);
    }

    public void SetBackgroundVolume(float soundValue)
    {
        masterMixer.SetFloat("BackgroundVol", soundValue);
    }

    public void SetSFXVolume(float soundValue)
    {
        masterMixer.SetFloat("SFXVol", soundValue);
    }
}
