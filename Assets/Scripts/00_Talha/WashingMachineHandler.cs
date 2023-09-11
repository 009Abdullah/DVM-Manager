using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WashingMachineHandler : MonoBehaviour
{
    [SerializeField] private ItemsDataContainer DropItemContainer, PickItemContainer;
    [SerializeField] private Transform PickUpItemPos;

    private void Update()
    {
        int index = DropItemContainer.Items.Count - 1;

        if (DropItemContainer.Items.Count != 0)
        {
            DropItemContainer.Items[index].transform.SetParent(PickUpItemPos);
            
            if (PickItemContainer.Items.Count == 0)
            {
                DropItemContainer.Items[index].transform.position = PickUpItemPos.position;
            }
            else
            {
                DropItemContainer.Items[index].transform.position = PickItemContainer.Items[^1].transform.position + new Vector3(0,0.5f,0);
            }
            
            PickItemContainer.Items.Add(DropItemContainer.Items[index]);

            DropItemContainer.Items.Remove(DropItemContainer.Items[index]);
        }
        
        
    }
}
