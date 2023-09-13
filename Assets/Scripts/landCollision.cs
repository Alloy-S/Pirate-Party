using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landCollision : MonoBehaviour
{
    void onCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "BasicBullet") {
            Destroy(collision.gameObject);
        }
    }
}
