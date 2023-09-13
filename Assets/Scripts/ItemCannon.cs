using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCannon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject listItems;
    public float itemForce = 10f;
    public List<GameObject> items;
    [SerializeField]
    private float timeRemaining = 5f;
    public bool isRotating;
    public bool rotating360;
    private float angle = 0f;
    private float direction;
    public float rotationSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        direction = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManajer.getInstance().getGameState() == GameState.playing)
        {
            if (isRotating)
            {
                // Debug.Log(angle);
                angle += direction * Time.deltaTime;
                if (angle > 120 && angle < 121)
                {
                    direction = -1 * rotationSpeed;
                    Debug.Log("reverse");
                }
                else if (angle < 315 && angle > 314)
                {
                    direction = 1 * rotationSpeed;
                }

                if (angle > 360)
                {
                    angle -= 360;
                }
                else if (angle < 0)
                {
                    angle += 360;
                }

                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            if (rotating360) {
                angle += direction * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                shootItem(items[Random.Range(0, items.Count)]);
                timeRemaining = 10f;

            }
        }
    }

    void shootItem(GameObject itemPrefab)
    {
        GameObject item = Instantiate(itemPrefab, firePoint.position, firePoint.rotation);
        item.transform.parent = GameObject.Find("List Items").transform;
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * itemForce, ForceMode2D.Impulse);
    }
}
