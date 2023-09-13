using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Boat : MonoBehaviour
{

    public float speed = 5.0f;
    private float angle;
    public Transform spriteTransform;
    private float HP = 100f;
    public static Player1Boat instance;
    public GameObject destroyEffect;
    private bool alive = true;
    private int wKeyPressCount = 0;
    public GameObject deadBoat;
    private float direction;
    public GameObject ammoDisplay;
    

    public static Player1Boat getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        direction = 1;
        angle = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManajer.getInstance().getGameState() == GameState.playing)
        {
            // GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            if (Input.GetKey(KeyCode.W))
            {
                angle = AngleCorection(angle + (0.5f * direction));
                // transform.eulerAngles -= new Vector3(0f, 0f, 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                wKeyPressCount++;

                if (wKeyPressCount >= 2)
                {
                    angle += 180;
                    wKeyPressCount = 0; // Reset the count after rotation
                }

                StartCoroutine(ResetWKeyPressCount());
            }

            if (HP <= 0 && alive)
            {
                alive = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GameObject shipEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(shipEffect, 0.8f);
                Instantiate(deadBoat, transform.position, transform.rotation);
                Destroy(ammoDisplay, 0.81f);
                Destroy(gameObject, 0.81f);
                
            }
        } else {
            // GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        // Debug.Log(angle);
    }

    void FixedUpdate()
    {
        if (HP > 0 && GameManajer.getInstance().getGameState() == GameState.playing)
        {
            float moveX = Mathf.Cos((angle + 90f) * Mathf.Deg2Rad);
            float moveY = Mathf.Sin((angle + 90f) * Mathf.Deg2Rad);

            Vector2 movement = new Vector2(moveX, moveY) * speed * Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().velocity = movement;

            // Set the rotation angle for the sprite
            spriteTransform.rotation = Quaternion.Euler(0, 0, angle);
        } else if (GameManajer.getInstance().getGameState() == GameState.pause) {
            // spriteTransform.rotation = Quaternion.Euler(0, 0, angle);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Reverse Item")
        {
            changeDirection();
            Player2Boat.getInstance().changeDirection();
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ResetWKeyPressCount()
    {
        yield return new WaitForSeconds(0.2f);
        wKeyPressCount = 0;
    }

    public float AngleCorection(float angle)
    {
        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    public void shipHit(float damage)
    {
        HP -= damage;
    }

    public void changeDirection()
    {
        if (this.direction == 1)
        {
            this.direction = -1;
        }
        else if (this.direction == -1)
        {
            this.direction = 1;
        }
        else
        {
            Debug.Log("terdapat kesalahan pada direction (" + direction + ")");
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
