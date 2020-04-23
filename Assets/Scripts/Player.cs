using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingx = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] float paddingy = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringCD = 0.1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [Header("Player Audio")]
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine strelbaCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - paddingx;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + paddingx;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - paddingy;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + paddingy;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        // AD left right
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // WS forward back

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    public int GetHealth()
    {
        return health;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            strelbaCoroutine = StartCoroutine(ExampleCoroutine());
            //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            //laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(strelbaCoroutine);
        }
    }
    IEnumerator ExampleCoroutine()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringCD);
            PlayShootSFX();
        }
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
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            PlayDeathSFX();
            ResetGame();
        }
    }
    private void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
    private void PlayShootSFX()
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
    public void ResetGame()
    {
        FindObjectOfType<sceneSwap>().LoadGameOverScene();
    }
    //IEnumerator cdd()
    //{
    //    yield return new WaitForSeconds(1);
    //}
}

// void Start () {
// StartCoroutine(PrintAndWait());
// }
// IEnumerator PrintAndWait()
//{
//yield return new WaitForSeconds(3);
//}
//
