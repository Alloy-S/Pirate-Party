using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    private float angle = 90f;
    public Transform spriteTransform;
    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) {
            angle = AngleCorection(angle + 0.5f);
            // transform.eulerAngles -= new Vector3(0f, 0f, 0.5f);
        }

        Debug.Log(angle);
    }

    void FixedUpdate()
    {
        float moveX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float moveY = Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector2 movement = new Vector2(moveX, moveY) * speed * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().velocity = movement;

        // Set the rotation angle for the sprite
        spriteTransform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public float AngleCorection(float angle) {
        if(angle > 360f) {
            angle -= 360f;
        }

        return angle;
    }
}
