using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get { return instance; }
    }


    private InputMaster inputs;

    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        inputs = new InputMaster();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public Vector2 GetMovementPlayer()
    {
        return inputs.Player.Movement.ReadValue<Vector2>();
    }
}
