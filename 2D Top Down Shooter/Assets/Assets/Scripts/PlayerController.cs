using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //CONTROLS
    public ControlType controlType;

    //MOBILE
    [SerializeField] private Joystick joystick;

    private InputManager inputs;
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public enum ControlType {PC, Android}

    private void Start()
    {
        inputs = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();

        if(controlType == ControlType.PC)
            joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(controlType == ControlType.PC)
            moveInput = inputs.GetMovementPlayer();

        else if (controlType == ControlType.Android)
            moveInput = new(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
