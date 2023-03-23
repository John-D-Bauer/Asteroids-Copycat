using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private float speed;

    private float xBound = 10.8f;
    private float yBound = 5.2f;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        speed = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        OffScreenTransportation();
    }

    // If rock moves offscreen, transports to opposite side
    private void OffScreenTransportation()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        else if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    // Moves rock forward
    private void MoveForward()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    // When rock collides with bullet, bullet is destroyed, rock splits
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            spawnManager.SpawnSmallerRocks(gameObject);
            Destroy(gameObject);
        }
    }
}
