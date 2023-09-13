using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAmmo : MonoBehaviour
{

    public Transform centerPoint;
    public GameObject ammo1, ammo2, ammo3;
    public float radius;
    public float speed;

    private float angle1 = 0f;
    private float angle2 = 90f;
    private float angle3 = 180f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (centerPoint != null)
        {
            // Calculate the new position based on the angle and radius
            Vector3 newPosition1 = centerPoint.position + new Vector3(Mathf.Cos(angle1) * radius, Mathf.Sin(angle1) * radius, 0);
            Vector3 newPosition2 = centerPoint.position + new Vector3(Mathf.Cos(angle2) * radius, Mathf.Sin(angle2) * radius, 0);
            Vector3 newPosition3 = centerPoint.position + new Vector3(Mathf.Cos(angle3) * radius, Mathf.Sin(angle3) * radius, 0);

            // Update the object's position
            ammo1.transform.position = newPosition1;
            ammo2.transform.position = newPosition2;
            ammo3.transform.position = newPosition3;

            // Increment the angle based on the speed
            angle1 += speed * Time.deltaTime;
            angle2 += speed * Time.deltaTime;
            angle3 += speed * Time.deltaTime;

            // Keep the angle between 0 and 360 degrees
            if (angle1 >= 360.0f)
            {
                angle1 -= 360.0f;
            }

            if (angle2 >= 360.0f)
            {
                angle2 -= 360.0f;
            }

            if (angle3 >= 360.0f)
            {
                angle3 -= 360.0f;
            }

        }
    }
}
