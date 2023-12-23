using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject prefabToInstantiate;
    public float spawnInterval = 5f;
    public float timetostart = 5f;

    private void Start()
    {
        // Start invoking the SpawnPrefab function with the specified interval
        StartCoroutine(StartDelayAndSpawn());
    }

    IEnumerator StartDelayAndSpawn()
    {
        yield return new WaitForSecondsRealtime(timetostart);

        // Start invoking the SpawnPrefab function with the specified interval
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    private void SpawnPrefab()
    {
        // Instantiate the prefab at the spawner's position and rotation
        Instantiate(prefabToInstantiate, FirePoint.position, FirePoint.rotation);
    }
}
