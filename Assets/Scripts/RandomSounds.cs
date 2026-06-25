using System.Collections;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Vector2 randomInterval;
    [SerializeField, Range(0, 1)] private float[] volumes;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySounds());
    }
    private IEnumerator PlaySounds()
    {
        if (audioClips != null && volumes.Length == audioClips.Length)
            while (true)
            {
                int index = Random.Range(0, audioClips.Length - 1);
                audioSource.PlayOneShot(audioClips[index], volumes[index]);
                yield return new WaitForSeconds(audioClips[index].length);
                yield return new WaitForSeconds(Random.Range(randomInterval.x, randomInterval.y));
            }
    }
}