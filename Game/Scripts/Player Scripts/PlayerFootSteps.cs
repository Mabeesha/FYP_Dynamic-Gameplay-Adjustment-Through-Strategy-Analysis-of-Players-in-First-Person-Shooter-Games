using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_sound;

    [SerializeField] private AudioClip[] footstep_clip;

    private CharacterController characterController;

    [HideInInspector]public float volume_Min=5f, volume_Max=10f;

    public float accumulated_Distance;

    [HideInInspector] public float step_Distance=0.8f;


         

    // Start is called before the first frame update
    void Awake() 
    {
        //step_Distance = 0.5f;
        footstep_sound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        //print("ssssssssssssssssssssssssssss");
        if (characterController.velocity.sqrMagnitude > 0)
        {
            //accumilated distance is the value how far we can move
            
            accumulated_Distance += Time.deltaTime ;
            if (accumulated_Distance>step_Distance)
            {
                print("Sound playing");
                footstep_sound.volume = Random.Range(volume_Min,volume_Max);
                footstep_sound.clip = footstep_clip[Random.Range(0,footstep_clip.Length)];
                footstep_sound.Play();

                accumulated_Distance = 0f;

            }
            else
            {
                accumulated_Distance = 0f;
            }
        }
    }


}
