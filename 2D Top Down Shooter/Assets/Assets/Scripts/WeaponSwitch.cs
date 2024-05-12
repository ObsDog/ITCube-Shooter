using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] allWeapons;

    //INPUT
    private InputManager inputs;

    private void Start()
    {
        inputs = InputManager.Instance;
    }

    private void Update()
    {
        if(inputs.SwitchWeapon())
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (allWeapons[i].activeInHierarchy == true)
                {
                    allWeapons[i].SetActive(false);

                    if(i != 0)
                        allWeapons[i - 1].SetActive(true);
                    else
                        allWeapons[^1].SetActive(true);
                    break;
                }
            }
        }
    }
}
