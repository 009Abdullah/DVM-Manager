using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

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

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Delay());
            }
            
        }
    }

    IEnumerator Delay()
    {
        if (m_SpawnedItemsData.Items.Count == 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        int index = m_SpawnedItemsData.Items.Count - 1;

        if (index >= 0)
        {
            m_SpawnedItemsData.Items[index].transform.SetParent(m_Playerdata.playerHand);

            if (m_PlayerItemsDataContainer.Items.Count == 0)
            {
                m_SpawnedItemsData.Items[^1].transform.position = m_Playerdata.playerHand.position;
            }
            else
            {
                m_SpawnedItemsData.Items[^1].transform.position =
                    m_PlayerItemsDataContainer.Items[^1].transform.position + new Vector3(0, 0.5f, 0);
            }

            m_PlayerItemsDataContainer.Items.Add(m_SpawnedItemsData.Items[index]);

            m_SpawnedItemsData.Items.Remove(m_SpawnedItemsData.Items[index]);

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
