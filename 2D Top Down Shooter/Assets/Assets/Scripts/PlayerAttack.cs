using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool IsAttacking => timeBetweenAttack <= 0 && (inputs.Attack() || (joystick.Horizontal != 0 || joystick.Vertical != 0));

    //INPUTS
    [SerializeField] private Joystick joystick; //MOBILE
    private InputManager inputs; //PC

    [SerializeField] private float startTimeBetweenAttack;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [SerializeField] private Transform attackPos;
    [SerializeField] private Animator anim;
    private PlayerController player;

    [SerializeField] private LayerMask enemy;

    private float timeBetweenAttack;

    private void Start()
    {
        inputs = InputManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if(player.controlType == PlayerController.ControlType.PC)
            joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(IsAttacking)
        {
            anim.SetTrigger("attack");
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
            timeBetweenAttack -= Time.deltaTime;
    }

    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
