using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipWeapons : MonoBehaviour
{
    [SerializeField] GameObject Sword;
    [SerializeField] GameObject Bow;
    [SerializeField] GameObject SwordWeaponHolder;
    [SerializeField] GameObject SwordWeaponSheath;
    [SerializeField] GameObject BowWeaponHolder;
    [SerializeField] GameObject BowWeaponSheath;

    GameObject SwordHolder;
    GameObject BowHolder;
    GameObject SwordSheath;
    GameObject BowSheath;

    void Start()
    {
        SwordSheath = Instantiate(Sword, SwordWeaponSheath.transform);
        BowSheath = Instantiate(Bow, BowWeaponSheath.transform);
    }

    //Calls the Equip and Unequip functions for the Sword
    public void EquipSword()
    {
        if (SwordHolder != null)
        {
            Destroy(SwordHolder);
        }

        if (SwordSheath != null)
        {
            Destroy(SwordSheath);
        }

        SwordHolder = Instantiate(Sword, SwordWeaponHolder.transform);
        SwordSheath = null;
    }

    public void UnequipSword()
    {
        if (SwordHolder != null)
        {
            Destroy(SwordHolder);
        }

        if (SwordSheath != null)
        {
            Destroy(SwordSheath);
        }

        SwordSheath = Instantiate(Sword, SwordWeaponSheath.transform);
        SwordHolder = null;
    }

    //Calls the Equip and Unequip functions for the Bow
    public void EquipBow()
    {
        if (BowHolder != null)
        {
            Destroy(BowHolder);
        }

        if (BowSheath != null)
        {
            Destroy(BowSheath);
        }

        BowHolder = Instantiate(Bow, BowWeaponHolder.transform);
        BowSheath = null;
    }

    public void UnequipBow()
    {
        if (BowHolder != null)
        {
            Destroy(BowHolder);
        }

        if (BowSheath != null)
        {
            Destroy(BowSheath);
        }

        BowSheath = Instantiate(Bow, BowWeaponSheath.transform);
        BowHolder = null;
    }
}