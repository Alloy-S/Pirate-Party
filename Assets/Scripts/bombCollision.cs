using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombCollision : MonoBehaviour
{

    public GameObject bombExplotion;
    public bool enableBomb = false;
    public float timeRemaining = 5f;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            enableBomb = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("explode");
        if (enableBomb)
        {
            if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
            {
                // StartCoroutine(explodeBomb(collision));
                GameObject bombEffect = Instantiate(bombExplotion, transform.position, transform.rotation);

                if (collision.gameObject.tag == "Player1")
                {
                    collision.gameObject.GetComponent<Player1Boat>().shipHit(500f);
                }

                if (collision.gameObject.tag == "Player2")
                {
                    collision.gameObject.GetComponent<Player2Boat>().shipHit(500f);
                }
                Destroy(transform.parent.gameObject);
                Destroy(bombEffect, 0.25f);
            }
        }

    }

    IEnumerator explodeBomb(Collider2D collision)
    {
        yield return new WaitForSeconds(1.5f);
        GameObject bombEffect = Instantiate(bombExplotion, transform.position, transform.rotation);

        if (collision.gameObject.tag == "Player1")
        {
            collision.gameObject.GetComponent<Player1Boat>().shipHit(500f);
        }

        if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Player2Boat>().shipHit(500f);
        }
        Destroy(gameObject);
        Destroy(bombEffect, 0.25f);
    }
}
