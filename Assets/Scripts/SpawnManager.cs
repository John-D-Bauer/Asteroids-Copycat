using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    // Max coordinates where objects can spawn
    private float xBound = 10.8f;
    private float yBound = 5.2f;

    // Objects to spawn
    [SerializeField] private GameObject[] rockPrefabs;
    [SerializeField] private GameObject bulletPrefab;

    int waveNum = 1;

    int activeEnemyCount;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        SpawnWave(waveNum);

        activeEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        activeEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (activeEnemyCount <= 0)
        {
            waveNum++;
            SpawnWave(waveNum);
        }

        //If player isn't dead, spawn bullet when space is pressed
        if(Input.GetKeyDown(KeyCode.Space) && player.GetComponent<SpriteRenderer>().enabled)
        {
            SpawnBullet();
        }
    }

    // Returns Vector2 with random coordinates in spawn range
    private Vector2 CreateRandomSpawnPosition()
    {
        float randomX;
        float randomY;

        do
        {
            randomX = Random.Range(-xBound, xBound);
            randomY = Random.Range(-yBound, yBound);
        }
        while (Mathf.Abs(randomX) < 2 && Mathf.Abs(randomY) < 2);

        return new Vector2(randomX, randomY);
    }

    // Creates rock object
    private void SpawnRock(GameObject spawnObject, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        Instantiate(spawnObject, spawnPosition, spawnRotation);
    }

    // Spawns wave of random rock objects
    private void SpawnWave(int waves)
    {
        for (int i = 0; i < waves; i++)
        {
            GameObject rockSpawned = rockPrefabs[Random.Range(0, rockPrefabs.Length)];

            Vector2 spawnPos = CreateRandomSpawnPosition();

            // Find vector from rock to player
            Vector3 relativePos = player.transform.position - (Vector3) spawnPos;

            // Find angle to rotate
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;

            // Create random rotation to avoid player
            Quaternion randomAngle = Quaternion.AngleAxis(Random.Range(angle + 20, angle + 340), Vector3.forward);

            SpawnRock(rockSpawned, spawnPos, randomAngle);

            activeEnemyCount++;
        }
    }

    // Spanws bullet from player
    private void SpawnBullet()
    {
        Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
    }

    // When there is collision, split rock into smaller rocks
    public void SpawnSmallerRocks(GameObject biggerRock)
    {
        GameObject smallerRock;

        if(biggerRock.name.Equals("Small Rock") || biggerRock.name.Equals("Small Rock(Clone)"))
        {
            Destroy(biggerRock);
            activeEnemyCount--;
        }
        else
        {
            if (biggerRock.name.Equals("Big Rock") || biggerRock.name.Equals("Big Rock(Clone)"))
            {
                smallerRock = rockPrefabs[1];
            }
            else
            {
                smallerRock = rockPrefabs[2];
            }

            for (int i = 0; i < 2; i++)
            {
                Quaternion randomAngle = GetRandomAngle();
                SpawnRock(smallerRock, biggerRock.transform.position, randomAngle);
            }
        }
    }

    private Quaternion GetRandomAngle()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}
