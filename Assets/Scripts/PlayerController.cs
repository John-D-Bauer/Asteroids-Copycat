using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float translateSpeed = 30.0f;
    [SerializeField] private float rotateSpeed = 180.0f;
    [SerializeField] private float horizontalInput;
    [SerializeField] private Rigidbody2D playerRb;

    private SpawnManager spawnManager;

    // Indicates bounds of screen
    private float xBound = 10.8f;
    private float yBound = 5.2f;

    public ParticleSystem playerExplosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to objects outside script
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        OffScreenTransportation();

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Manages player movement
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime * horizontalInput);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerRb.AddRelativeForce(Vector2.up * translateSpeed * Time.deltaTime);
        }

        if(horizontalInput == 0)
        {
            playerRb.angularVelocity = 0;
        }
    }

    // If player moves offscreen, transports to opposite side
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


    // When player collides with rock, destroy player, and split rock
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            playerExplosionParticle.Play(true);
            spawnManager.SpawnSmallerRocks(collision.gameObject);
            Destroy(collision.gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
}

