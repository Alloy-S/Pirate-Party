using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemProperty : MonoBehaviour
{
    public static ItemProperty instance;
    private float HP = 10;

    public static ItemProperty getInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        
    }

    void OnCollisionExit2D(Collision2D collision) {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), false);
    }

    public void getDamage(float damage)
    {
        HP -= damage;
    }
}
