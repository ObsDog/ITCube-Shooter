using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    //CONTROL TYPE
    public ControlType controlType;

    //MOBILE CONTROL
    [SerializeField] private Joystick joystick;

    //DESKTOP CONTROL
    private InputManager inputs;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private bool facingRight = true;

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

        //FLIP PLAYER
        if (!facingRight && moveInput.x > 0)
            Flip();
        else if(facingRight && moveInput.x < 0)
            Flip();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
