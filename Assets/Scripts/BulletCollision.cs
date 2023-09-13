using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public Vector2 velocity;

    void Update() {
        // if (GameManajer.getInstance().getGameState() == GameState.playing){
        //     GetComponent<Rigidbody2D>().velocity = velocity;
        //     velocity = GetComponent<Rigidbody2D>().velocity;
        // } else {
            
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        // }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (collision.gameObject.tag != "Border")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
        }
        

        if (collision.gameObject.tag == "Player1")
        {
            collision.gameObject.GetComponent<Player1Boat>().shipHit(25f);
        }

        if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Player2Boat>().shipHit(25f);
        }

        if (obj.tag == "Reverse Item" || obj.tag == "TripleShoot Item" || obj.tag == "Bomb") {
            // Debug.Log("duar");
            Destroy(obj);
            // ItemProperty.getInstance().getDamage(25f);
        }

        
        // GameObject shipEffect = Instantiate(destroyEffect, collision.transform.position, Quaternion.identity);
        // Destroy(shipEffect, 0.8f);
        // Destroy(collision.gameObject, 0.8f);
        Destroy(gameObject);
    }

    
}
