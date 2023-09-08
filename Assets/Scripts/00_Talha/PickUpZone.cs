using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpZone : MonoBehaviour
{
    [SerializeField] private ItemsDataContainer m_SpawnedItemsData;
    private ItemsDataContainer m_PlayerItemsDataContainer = new();
    private PlayerDataContainer m_Playerdata = new();
    private Vector3 SpawnPosition;
    private Coroutine _coroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get Player Container
            m_PlayerItemsDataContainer = other.GetComponent<ItemsDataContainer>();
            
            m_Playerdata = other.GetComponent<PlayerDataContainer>();
            
            _coroutine = StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        if (m_SpawnedItemsData.Items.Count <= 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        
        if (m_PlayerItemsDataContainer.Items.Count == 0)
        {
            SpawnPosition = Vector3.zero;
        }
        else
        {
            SpawnPosition = m_PlayerItemsDataContainer.Items[m_PlayerItemsDataContainer.Items.Count - 1].transform.position + new Vector3(0, 0.5f, 0);
        }
        

        int index = m_SpawnedItemsData.Items.Count - 1;

        m_SpawnedItemsData.Items[index].transform.SetParent(m_Playerdata.playerHand);
        
        m_PlayerItemsDataContainer.Items.Add(m_SpawnedItemsData.Items[index]);
        
        m_SpawnedItemsData.Items.Remove(m_SpawnedItemsData.Items[index]);

        //m_SpawnedItemsData.Items[index].transform.DOMove(SpawnPosition, 0.5f);

        m_PlayerItemsDataContainer.Items[m_PlayerItemsDataContainer.Items.Count - 1].transform.position = Vector3.zero;
        m_PlayerItemsDataContainer.Items[m_PlayerItemsDataContainer.Items.Count - 1].transform.localPosition = SpawnPosition;
        
        yield return new WaitForSeconds(0.5f);

        _coroutine = StartCoroutine(Delay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
