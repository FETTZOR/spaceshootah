using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 222;

    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [Header("Enemy Audio")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    void Update()
    {
        CountDownAndShoot();
        //Hit();
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            ShootSFX();
        }    
    }
   private void Fire()
    {
        GameObject laser = Instantiate(
       projectile,
         transform.position,
         Quaternion.identity
    ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }
    private void ShootSFX()
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDeal damageDealer = other.gameObject.GetComponent<DamageDeal>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDeal damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        PlayDeathSFX();
    }
    private void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
    //private void HandleHit()
    //{
    //    timesHit++;
    //    if (timesHit >= maxHits)
    //    {
    //        DestroyBlock();
    //    }
    //    else
    //    {
    //        ShowNextHitSprite();
    //    }
    //}
    //public void Hit()
    //{
    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
