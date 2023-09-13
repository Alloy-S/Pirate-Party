using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLimit : MonoBehaviour
{
   void OnTriggerExit2D(Collider2D collision) {
        // Debug.Log("keluar destroy");
        Destroy(collision.gameObject);
    }
}
