using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool IsDesktop => player.controlType == PlayerController.ControlType.PC;

    [SerializeField] private GunType gunType;

    [SerializeField] private float offset;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    private PlayerController player;

    //INPUTS
    [SerializeField] private Joystick joystick; //MOBILE
    private InputManager inputs; //PC

    //SHOOTING
    [SerializeField] private float startTimeBetweenShots;
    private float timeBetweenShots;

    //GUN ROTATION
    float rotZ;
    Vector3 difference;

    //GUN_TYPE
    public enum GunType {Default, Enemy}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inputs = InputManager.Instance;

        if(IsDesktop && gunType == GunType.Default)
            joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gunType == GunType.Default)
        {
            if (IsDesktop)
            {
                difference = Camera.main.ScreenToWorldPoint(inputs.GetMousePosition()) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (!IsDesktop && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        }
        else
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
            
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBetweenShots <= 0)
        {
            if (IsDesktop && inputs.Attack() || gunType == GunType.Enemy)
                Shoot();
            else
            {
                if(joystick.Horizontal != 0 || joystick.Vertical != 0)
                    Shoot();
            }
        }
        else
            timeBetweenShots -= Time.deltaTime;

    }

    public void Shoot()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        timeBetweenShots = startTimeBetweenShots;
    }
}
