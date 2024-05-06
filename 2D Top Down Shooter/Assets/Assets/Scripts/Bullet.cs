using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask whatIsSolid;

    //CHANGE BULLET TO ENEMY_BULLET
    [SerializeField] private bool enemyBullet = false;

    //VFX
    [SerializeField] private GameObject bulletEffect;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hit.collider != null && hit.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (hit.collider != null && hit.collider.TryGetComponent(out PlayerController player) && enemyBullet)
        {
            player.ChangeHealth(-damage);
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
            Invoke(nameof(DestroyGO), lifetime);

        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }
}
