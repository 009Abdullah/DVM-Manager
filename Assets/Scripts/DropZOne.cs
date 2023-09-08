using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZOne : MonoBehaviour
{
    private GameObject pickedCube;
    private Coroutine _coroutine;
    public Transform SpawnPoint;
    [SerializeField]private List<GameObject> SpawnCubes = new List<GameObject>();

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("enter the trigger");
            yield return new WaitForSeconds(0f);
            print("Enter the pick trigger");
            CubePickup obj = other.GetComponent<CubePickup>();
            if (_coroutine==null)
            {
                _coroutine = StartCoroutine(pickCubes(obj));
            }
        }
    }
    IEnumerator pickCubes(CubePickup obj)
    {
        if (obj.SpawnCubes.Count == 0)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        obj.SpawnCubes[obj.SpawnCubes.Count-1].transform.SetParent(SpawnPoint);
        obj.SpawnCubes[obj.SpawnCubes.Count - 1].transform.position = new Vector3(0, 0, 0);
        
        yield return new WaitForSeconds(.01f);

        if (SpawnCubes.Count==0)
        {
            obj.SpawnCubes[obj.SpawnCubes.Count-1].transform.position = SpawnPoint.position;
        }
        else
        {
            obj.SpawnCubes[obj.SpawnCubes.Count-1].transform.position = SpawnCubes[SpawnCubes.Count - 1].transform.position + new Vector3(0,0.7f,0);
        }
        
        
        SpawnCubes.Add(obj.SpawnCubes[obj.SpawnCubes.Count-1]);
        obj.SpawnCubes.Remove(obj.SpawnCubes[obj.SpawnCubes.Count - 1]);
        yield return new WaitForSeconds(1f);
        _coroutine = StartCoroutine(pickCubes(obj));
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
