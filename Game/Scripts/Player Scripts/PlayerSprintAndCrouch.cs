using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_speed = 10f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform look_Root;

    private float stand_height = 1.6f;

    private float crouch_heigt = 1f;
    private bool is_Crouching;

    private PlayerFootSteps playerFootSteps;

    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_valume_min = 0.2f, walk_volume_max = 0.6f;

    private float walkstep_distance=0.4f;
    private float sprintstep_distance = 0.25f;

    private float crouchstep_distance = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();


    }

    private void Start()
    {
        playerFootSteps.volume_Min = walk_valume_min;
        playerFootSteps.volume_Max = walk_volume_max;
        playerFootSteps.step_Distance = walkstep_distance;

    }

    // Update is called once per frame
    void Update()
    {
        sprint();
        Crouch();
    }

    void sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&& !is_Crouching)
        {
            playerMovement.speed = sprint_speed;

            playerFootSteps.step_Distance = sprintstep_distance;
            playerFootSteps.volume_Min = sprint_volume;
            playerFootSteps.volume_Max = sprint_volume;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = move_speed;

            playerFootSteps.step_Distance = walkstep_distance;
            playerFootSteps.volume_Min = walk_valume_min;
            playerFootSteps.volume_Max = walk_volume_max;
        }

    } // sprint

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f,stand_height,0f);
                playerMovement.speed = move_speed;
                is_Crouching = false;

                playerFootSteps.step_Distance = walkstep_distance;
                playerFootSteps.volume_Min = walk_valume_min;
                playerFootSteps.volume_Max = walk_volume_max;

            }
            else
            {

                is_Crouching = true;
                look_Root.localPosition = new Vector3(0f, crouch_heigt, 0f);
                playerMovement.speed = crouch_heigt;

                playerFootSteps.step_Distance = crouchstep_distance;
                playerFootSteps.volume_Min = crouch_volume;
                playerFootSteps.volume_Max = crouch_volume;
            }
        }


    } //crouch

}
