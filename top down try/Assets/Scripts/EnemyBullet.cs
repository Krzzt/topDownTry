using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] ParticleSystem onHitParticle;
    public Boolean destroy = false;
    public float damage = 5;
    GameObject spawner;
    Spawner spawn;


    private void Awake()
    {
        spawner = GameObject.FindWithTag("WaveSpawner");
        spawn = spawner.GetComponent<Spawner>();
        damage = damage * spawn.EnemyHealthMultiplier;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Explode(gameObject.transform.position);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerBullet") && destroy)
        {
            Explode(gameObject.transform.position);
            Destroy(gameObject);
           
        }
    }



    public void Destruction()
    {
        destroy = true;
        
    }

    public void Explode(Vector2 position)
    {
        Instantiate(onHitParticle, position, Quaternion.identity);

    }
}
