using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Handler : MonoBehaviour {

    private vp_PlayerItemDropper _itemDropper;

	// Use this for initialization
	void Start () {
        _itemDropper = GetComponent<vp_PlayerItemDropper>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.B))
        {
            _itemDropper.TryDropCurrentWeapon();
        }
		
	}
}
