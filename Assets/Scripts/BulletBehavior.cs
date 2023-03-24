using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    private float xBound = 10.8f;
    private float yBound = 5.2f;

    private GameObject player;
    private CircleCollider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletCollider = GetComponent<CircleCollider2D>();
        bulletCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        DestroyOutOfBounds();

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer > 0.6)
        {
            bulletCollider.enabled = true;
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
   
    private void DestroyOutOfBounds()
    {
        if (Mathf.Abs(transform.position.x) > xBound)
        {
            Destroy(gameObject);
        }
        else if(Mathf.Abs(transform.position.y) > yBound)
        {
            Destroy(gameObject);
        }
    }

}
