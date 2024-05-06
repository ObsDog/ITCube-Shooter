using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;

    //ATTACK
    [SerializeField] private float startTimeBetweenAttack;
    [SerializeField] private float damage;
    private float timeBetweenAttack;

    //TAKE_DAMAGE
    [SerializeField] private float startStopTime;
    [SerializeField] private float normalSpeed;
    private float stopTime;

    private PlayerController player;
    private Animator anim;

    //VFX
    [SerializeField] private GameObject deathEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (stopTime <= 0) speed = normalSpeed;
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0) 
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
        if (player.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.magnitude != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    public void TakeDamage(float damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(timeBetweenAttack <= 0)
                anim.SetTrigger("enemyAttack");
            else
                timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void OnEnemyAttack()
    {
        Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        player.ChangeHealth(-damage);
        timeBetweenAttack = startTimeBetweenAttack;
    }
}
