using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;

    public ControlType controlType;

    //DESKTOP CONTROL
    private InputManager inputs;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private bool facingRight = true;

    [Header("Bonuses")]
    public float health = 100;
    public GameObject potionEffect;
    public GameObject shieldEffect;
    public GameObject shield;
    public Shield shieldTimer;

    public enum ControlType {PC, Android}

    private void Start()
    {
        inputs = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if(controlType == ControlType.PC)
            joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (controlType == ControlType.PC)
            moveInput = inputs.GetMovementPlayer();

        else if (controlType == ControlType.Android)
            moveInput = new(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0 && moveInput.y == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

        //FLIP PLAYER
        if (!facingRight && moveInput.x > 0)
            Flip();
        else if (facingRight && moveInput.x < 0)
            Flip();

        //DEATH 
        if (health <= 0) 
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (health > 100) health = 100;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Potion") && health < 100)
        {
            ChangeHealth(20);
            Instantiate(potionEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Debug.Log("Potion is used");
        }
        else if(other.CompareTag("Shield"))
        {
            if (!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                Instantiate(shieldEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Instantiate(shieldEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }

    public void ChangeHealth(float healthValue)
    {
        if (!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
            health += healthValue;

        else if (shield.activeInHierarchy && healthValue < 0)
            shieldTimer.ReduceTimer(healthValue);
    }
}
