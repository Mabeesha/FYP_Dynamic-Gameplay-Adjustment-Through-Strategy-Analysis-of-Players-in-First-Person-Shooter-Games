using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundTrigger : MonoBehaviour
{
    public GameObject AmbienceSoundObject;
    private void OnTriggerEnter()
    {
        AmbienceSoundObject.SetActive(false);
    }
}
