using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : vp_FPController
{

    private CharacterController characterController;

    protected override void Awake()
    {
        characterController = GetComponent<CharacterController>();
        base.Awake();
    }

    //protected over



}
