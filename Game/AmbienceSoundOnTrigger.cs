using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundOnTrigger : MonoBehaviour
{
    public GameObject AmbienceSoundObject;
    private void OnTriggerEnter()
    {
        AmbienceSoundObject.SetActive(true);
    }
}
