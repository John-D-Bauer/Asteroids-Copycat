using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    private float xBound = 10.8f;
    private float yBound = 5.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        DestroyOutOfBounds();
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
