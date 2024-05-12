using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private GameObject floatingDamage;

    //ATTACK
    [SerializeField] private float startTimeBetweenAttack;
    [SerializeField] private float damage;
    private float timeBetweenAttack;

    [HideInInspector] public bool playerNotInRoom;
    private bool stopped;

    //TAKE_DAMAGE
    [SerializeField] private float startStopTime;
    private float stopTime;

    private PlayerController player;
    private AddRoom room;
    private Animator anim;

    //VFX
    [SerializeField] private GameObject deathEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>();
        room = GetComponentInParent<AddRoom>();
    }

    private void Update()
    {
        if (!playerNotInRoom)
        {

            if (stopTime <= 0) stopped = false;
            else
            {
                stopped = true;
                stopTime -= Time.deltaTime;
            }
        }
        else stopped = true;

        if (health <= 0) 
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
            room.enemies.Remove(gameObject);

            if (isBoss && room.enemies.Count == 0) SceneManager.LoadScene(0);
        }

        if (player.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);

        if (!stopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (transform.position.magnitude != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    public void TakeDamage(float damage)
    {
        stopTime = startStopTime;
        health -= damage;
        Vector2 damagePos = new(transform.localPosition.x, transform.localPosition.y + 2.75f);
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        floatingDamage.GetComponent<FloatingDamage>().damage = damage;
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
