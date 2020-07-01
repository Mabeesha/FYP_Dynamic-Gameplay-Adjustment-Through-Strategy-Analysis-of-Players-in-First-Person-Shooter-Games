using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] public GameObject[] ammoLoots;
    public GameObject serverobject;
    private bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            enable_ammo_loots();
        }
        
    }

    private void enable_ammo_loots()
    {
        System.Random random = new System.Random();
        if (server.aggressivePercentage!=0)
        {
            Debug.Log("Valid aggressive perccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc");
            isEnabled = true;
            double ammo_loots_percentage = server.aggressivePercentage;

            int num_of_available_loots = (int)Math.Round((12 *(ammo_loots_percentage)), MidpointRounding.AwayFromZero);
            Debug.Log("Loots ::::::::::::::::::::::::::::::: "+ num_of_available_loots);
            int count = 0;
        
            for (int i= num_of_available_loots; i< 12; i++)
            {
                count++;
               // int  num = random.Next(11);
                if (ammoLoots[i]!=null)
                {
                    ammoLoots[i].SetActive(false);
                }
                
            }


        }
        
    }

}
