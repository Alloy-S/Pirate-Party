using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public Transform TriplefirePoint1;
    public Transform TriplefirePoint2;
    public Transform TriplefirePoint3;
    public Transform bombFirePoint;
    public GameObject bulletPrefab, bombPrefabs;
    private int ammo = 3;
    private int maxAmmo = 3;
    [SerializeField]
    private float timeRemaining = 2f;
    private bool triple = false;
    private bool bombReady = false;
    private int shootingMode = 0;
    private int tripleAmmo = 0;
    public GameObject ammo1, ammo2, ammo3;

    public float bulletForce = 15f;

    // Update is called once per frame
    void Update()
    {

        if (GameManajer.getInstance().getGameState() == GameState.playing)
        {
            // Debug.Log(ammo);
            if (Input.GetKeyDown(KeyCode.E) && this.tag == "Player1")
            {

                if (ammo > 0)
                {
                    AudioManager.instance.play("shoot1");
                    switch (shootingMode)
                    {
                        case 0:
                            shoot();
                            break;
                        case 1:
                            tripleShoot();
                            break;
                        case 2:
                             Debug.Log("ejected bomb");
                            ejectBomb();
                            bombReady = false;
                            shootingMode = 0;
                            break;
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.O) && this.tag == "Player2")
            {

                if (ammo > 0)
                {
                    AudioManager.instance.play("shoot1");
                    switch (shootingMode)
                    {
                        case 0:
                            shoot();
                            break;
                        case 1:
                            tripleShoot();
                            break;
                        case 2:
                            ejectBomb();
                            bombReady = false;
                            shootingMode = 0;
                            break;
                    }
                }
            }

            switch (ammo)
            {
                case 1:
                    ammo1.SetActive(true);
                    ammo2.SetActive(false);
                    ammo3.SetActive(false);
                    break;
                case 2:
                    ammo1.SetActive(true);
                    ammo2.SetActive(true);
                    ammo3.SetActive(false);
                    break;
                case 3:
                    ammo1.SetActive(true);
                    ammo2.SetActive(true);
                    ammo3.SetActive(true);
                    break;
                default:
                    ammo1.SetActive(false);
                    ammo2.SetActive(false);
                    ammo3.SetActive(false);
                    break;
            }

            if (tripleAmmo <= 0 && triple == true)
            {
                shootingMode = 0;
                triple = false;
            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                if (ammo + 1 <= maxAmmo)
                {
                    ammo += 1;
                }
                timeRemaining = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TripleShoot Item")
        {
            triple = true;
            shootingMode = 1;
            tripleAmmo = 3;
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "Bomb Item")
        {
            Debug.Log("get bomb");
            bombReady = true;
            shootingMode = 2;
            Destroy(collision.gameObject);
        }

        
    }

    void shoot()
    {

        ammo -= 1;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }

    void ejectBomb()
    {
        GameObject bomb = Instantiate(bombPrefabs, bombFirePoint.position, bombFirePoint.rotation);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        rb.AddForce(bombFirePoint.up * 5, ForceMode2D.Impulse);
    }

    void tripleShoot()
    {

        ammo -= 1;
        tripleAmmo -= 1;
        GameObject bullet1 = Instantiate(bulletPrefab, TriplefirePoint1.position, TriplefirePoint1.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, TriplefirePoint2.position, TriplefirePoint2.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab, TriplefirePoint3.position, TriplefirePoint3.rotation);

        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();

        rb1.AddForce(TriplefirePoint1.up * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(TriplefirePoint2.up * bulletForce, ForceMode2D.Impulse);
        rb3.AddForce(TriplefirePoint3.up * bulletForce, ForceMode2D.Impulse);

    }
}
