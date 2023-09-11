using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] private ItemsDataContainer m_SpawnedItemsData;
    [SerializeField] private Transform DropPos;
    private ItemsDataContainer m_PlayerItemsDataContainer = new();
    private Vector3 SpawnPosition;
    private Coroutine _coroutine;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get Player Container
            m_PlayerItemsDataContainer = other.GetComponent<ItemsDataContainer>();

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Delay());
            }
            
        }
    }

    IEnumerator Delay()
    {
        if (m_PlayerItemsDataContainer.Items.Count == 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        
        int index = m_PlayerItemsDataContainer.Items.Count - 1;

        print(index);
        if (index >= 0)
        {
            m_PlayerItemsDataContainer.Items[index].transform.SetParent(DropPos);
        
       
        
            if (m_SpawnedItemsData.Items.Count == 0)
            {
                m_PlayerItemsDataContainer.Items[^1].transform.position = DropPos.position;
            }
            else
            {
                m_PlayerItemsDataContainer.Items[^1].transform.position = m_SpawnedItemsData.Items[^1].transform.position + new Vector3(0, 0.5f, 0);
            }
        
        
            m_SpawnedItemsData.Items.Add(m_PlayerItemsDataContainer.Items[index]);
        
            m_PlayerItemsDataContainer.Items.Remove(m_PlayerItemsDataContainer.Items[index]);

            //m_SpawnedItemsData.Items[index].transform.DOMove(SpawnPosition, 0.5f);
        
            yield return new WaitForSeconds(0.5f);

            _coroutine = StartCoroutine(Delay());
        }
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
