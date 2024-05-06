using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] weapon;

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
            for (int i = 0; i < weapon.Length; i++)
            {
                if (weapon[i].activeInHierarchy == true)
                {
                    weapon[i].SetActive(false);

                    if(i != 0)
                        weapon[i - 1].SetActive(true);
                    else
                        weapon[^1].SetActive(true);
                    break;
                }
            }
        }
    }
}
