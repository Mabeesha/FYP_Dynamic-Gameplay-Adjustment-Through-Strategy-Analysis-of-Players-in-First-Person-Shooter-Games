using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]private AudioClip screamClip, dieClip;

    [SerializeField] private AudioClip[] attack_clips;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();


    }

    public void play_screamsound()
    {
        audioSource.clip = screamClip;
        audioSource.Play();
    }

    public void play_attacksound()
    {
        audioSource.clip = attack_clips[Random.Range(0,attack_clips.Length)];
    }

    public void play_deadsound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }



}
