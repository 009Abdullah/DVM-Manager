using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnItem;
    [SerializeField] private Transform m_SpawnItemPoint;
    [SerializeField] private int m_SpawnItemLimit;
    [SerializeField] private float m_SpawnItemSpeed;
    [SerializeField] private float m_SpawnItemOffset;
    [SerializeField] private ItemsDataContainer m_ItemDataContainer;
    private WaitForSeconds m_WaittoSpawnItem;
    private Vector3 SpwanPosition;
    private bool StopSpawning;

    private void Start()
    {
        m_WaittoSpawnItem = new WaitForSeconds(m_SpawnItemSpeed);
        SpwanPosition = m_SpawnItemPoint.position;
        StartCoroutine(SpwanItems());
    }
    

    IEnumerator SpwanItems()
    {
        yield return m_WaittoSpawnItem;

        while (m_ItemDataContainer.Items.Count < m_SpawnItemLimit)
        {
            var obj = Instantiate(m_SpawnItem, SpwanPosition, quaternion.identity, m_SpawnItemPoint);
            m_ItemDataContainer.Items.Add(obj);
            SpwanPosition += new Vector3(0,m_SpawnItemOffset,0);

        }
    }
}
