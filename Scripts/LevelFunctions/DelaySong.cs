using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DelaySong : MonoBehaviour
{
    public AudioSource Song;
    void Start ()
    {
        //timing the song to start w the notes
        StartCoroutine(PlaySoundAfterDelay(Song, 3.5f));
    }


    IEnumerator PlaySoundAfterDelay(AudioSource audioSource, float delay)
    {
        if (audioSource == null)
            yield break;
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
