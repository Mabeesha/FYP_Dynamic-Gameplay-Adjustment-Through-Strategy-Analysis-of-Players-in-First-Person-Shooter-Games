using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    private vp_FPPlayerEventHandler m_Player = null;
    private vp_FPWeaponMeleeAttack m_Mace = null;
    public GameObject m_Parent = null; // drag Player game object to this, in the inspector
    private Log file;
    float currentCount=0;
    string currentWeapon;
    float currentAmmo;

    void Awake()
    {
        m_Player =GetComponent < vp_FPPlayerEventHandler >();
        //m_Mace =GetComponentInChildren <vp_FPWeaponMeleeAttack>();
    }


    protected virtual void OnEnable()
    {

        if (m_Player != null)
            m_Player.Register(this);
    }



    protected virtual void OnDisable()
    {

        if (m_Player != null)
            m_Player.Unregister(this);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            string currentWeapon = m_Player.CurrentWeaponName.Get();
            Log.logByAnotherObject("Scoping_on : " + currentWeapon + "   ");
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            string currentWeapon = m_Player.CurrentWeaponName.Get();
            Log.logByAnotherObject("Scoping_off : " + currentWeapon + "   ");
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            currentWeapon = m_Player.CurrentWeaponName.Get();
            currentAmmo = m_Player.CurrentWeaponAmmoCount.Get();
            //m_Player.CurrentWeaponAmmoCount.Set(30);
            if (currentAmmo!=currentCount)
            {
                currentCount= m_Player.CurrentWeaponAmmoCount.Get();
                Log.logByAnotherObject("Shooting : " + currentWeapon + "  Current_Ammo count  :" + currentAmmo + "   ");

            }


        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentWeapon = m_Player.CurrentWeaponName.Get();
            if (currentWeapon=="5Knife")
            {
                Log.logByAnotherObject("Shooting : " + currentWeapon + "  : ");
            }
        }

    }
}
