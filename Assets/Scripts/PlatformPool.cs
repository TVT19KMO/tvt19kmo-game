using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformsPrefab;
    public int platformPoolSize = 6;
    public float spawnRate = 3;
    public float platformMinX = 0;
    public float plartformMaxX = 0;
    public float platformMinY = -2;
    public float platformMaxY = 2;

    private GameObject[] platforms;
    private Vector2 objectPoolPosition;
    private float timeSinceLastSpawned;
    private float spawnXPosition = 20f;
    private int currentPlatform = 0;

    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[platformPoolSize];
        for (int i=0; i<platformPoolSize; i++)
        {
            objectPoolPosition = new Vector2(5 * i, Random.Range(platformMinY, platformMaxY));
            platforms[i] = (GameObject)Instantiate(platformsPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControllerPlatformer.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;
            float spawnYPosition = Random.Range(platformMinY, platformMaxY);
            //float spawnXPosition = Random.Range(platformMinX, platformMaxX);
            platforms[currentPlatform].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentPlatform++;

            if (currentPlatform >= platformPoolSize)
            {
                currentPlatform = 0;
            }
        }
    }
}
