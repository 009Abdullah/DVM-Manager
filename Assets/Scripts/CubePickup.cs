using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CubePickup : MonoBehaviour
{
    private GameObject pickedCube; // Reference to the cube that the player picks up
    public Transform playerHand;
    private Coroutine _coroutine;
    public List<GameObject> SpawnCubes = new List<GameObject>();


    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick")) // Assuming your cube has a "Cube" tag
        {
            yield return new WaitForSeconds(0f);
            print("Enter the pick trigger");
            CubeStacker obj = other.GetComponent<CubeStacker>();
            if (_coroutine==null)
            {
                _coroutine = StartCoroutine(pickCubes(obj));
            }
            
            
        }
    }

    IEnumerator pickCubes(CubeStacker obj)
    {
        if (obj.spawnedCubes.Count == 0)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        obj.spawnedCubes[obj.spawnedCubes.Count-1].transform.SetParent(playerHand);
        obj.spawnedCubes[obj.spawnedCubes.Count - 1].transform.position = new Vector3(0, 0, 0);
        
        yield return new WaitForSeconds(.01f);

        if (SpawnCubes.Count==0)
        {
            obj.spawnedCubes[obj.spawnedCubes.Count-1].transform.position = playerHand.position;
        }
        else
        {
            obj.spawnedCubes[obj.spawnedCubes.Count-1].transform.position = SpawnCubes[SpawnCubes.Count - 1].transform.position + new Vector3(0,0.7f,0);
        }
        
        
        SpawnCubes.Add(obj.spawnedCubes[obj.spawnedCubes.Count-1]);
        obj.spawnedCubes.Remove(obj.spawnedCubes[obj.spawnedCubes.Count - 1]);
        yield return new WaitForSeconds(1f);
        _coroutine = StartCoroutine(pickCubes(obj));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pick"))
        {
            print("exit the trigger");
            if (_coroutine==null)
            {
                return;
            }
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}