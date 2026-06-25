using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSounds : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Vector2 randomInterval;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySounds());
    }
    private IEnumerator PlaySounds()
    {
        if (sounds == null)
            yield break;
        List<Sound> weightedSounds = new();
        foreach (Sound sound in sounds)
        {
            for (int i = 0; i < sound.weight; i++)
            {
                weightedSounds.Add(sound);
            }
        }

        while (true)
        {
            Sound sound = weightedSounds[Random.Range(0, weightedSounds.Count - 1)];

            audioSource.clip = sound.audioClip;
            audioSource.volume = sound.volume;
            audioSource.Play();

            yield return new WaitForSeconds(sound.audioClip.length);
            yield return new WaitForSeconds(Random.Range(randomInterval.x, randomInterval.y));
        }
    }
}

[Serializable]
public class Sound
{
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)] public float volume;
    [Min(0)] public int weight;
}