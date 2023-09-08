using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpZone : MonoBehaviour
{
    [SerializeField] private ItemsDataContainer m_SpawnedItemsData;
    private ItemsDataContainer m_PlayerItemsDataContainer = new();
    private CubePickup m_Playerdata = new();
    private Vector3 SpawnPosition = new();
    private Coroutine _coroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerItemsDataContainer = other.GetComponent<ItemsDataContainer>();
            m_Playerdata = other.GetComponent<CubePickup>();
            
            if (m_PlayerItemsDataContainer.Items.Count == 0)
            {
                SpawnPosition = new Vector3(0,0,0);
            }
            else
            {
                SpawnPosition = m_PlayerItemsDataContainer.Items[m_PlayerItemsDataContainer.Items.Count - 1].transform.position + new Vector3(0, 0.5f, 0);
            }
            

            _coroutine = StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        if (m_SpawnedItemsData.Items.Count < 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }


        if (m_SpawnedItemsData != null)
        {
            m_SpawnedItemsData.Items[m_SpawnedItemsData.Items.Count - 1].transform.SetParent(m_Playerdata.playerHand);

            var position = SpawnPosition;
            m_SpawnedItemsData.Items[^1].transform.DOMove(position, 0.5f);

            //m_SpawnedItemsData.Items[m_SpawnedItemsData.Items.Count - 1].transform.position = SpawnPosition.position;

            position += new Vector3(0, 0.5f, 0);
            SpawnPosition = position;

            m_PlayerItemsDataContainer.Items.Add(m_SpawnedItemsData.Items[m_SpawnedItemsData.Items.Count - 1]);
            m_SpawnedItemsData.Items.Remove(m_SpawnedItemsData.Items[m_SpawnedItemsData.Items.Count - 1]);
        }

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
