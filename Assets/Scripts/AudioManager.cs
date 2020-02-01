using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip bgmStart;
    [SerializeField] AudioClip bgmLoop;
    AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = false;
        StartCoroutine(WaitAndStartLoop());
    }

    IEnumerator WaitAndStartLoop()
    {
        yield return new WaitForSeconds(bgmStart.length);
        audioSrc.clip = bgmLoop;
        audioSrc.loop = true;
        audioSrc.Play();
    }

}
