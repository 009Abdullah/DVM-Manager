using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStacker : MonoBehaviour
{
    public GameObject cubePrefab;
    public int stackHeight = 10;
    public float stackSpacing = 0.1f;
    public float spawnDelay = 1.0f;
    public Transform SpawnPoint;
    public List<GameObject> spawnedCubes = new List<GameObject>();
    
    private Transform cubeStack;
    private void Start()
    {
        StartCoroutine(SpawnCubesWithDelay(SpawnPoint.position));
    }
 
    private IEnumerator SpawnCubesWithDelay(Vector3 spawnPosition)
    {
        for (int i = 0; i < stackHeight; i++)
        {
            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            cube.transform.SetParent(transform);
            
            spawnedCubes.Add(cube);

            spawnPosition.y += cube.transform.localScale.y + stackSpacing;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
