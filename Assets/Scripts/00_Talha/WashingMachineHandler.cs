using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WashingMachineHandler : MonoBehaviour
{
    [SerializeField] private ItemsDataContainer DropItemContainer, PickItemContainer;

    private void Update()
    {
        int index = DropItemContainer.Items.Count - 1;

        if (DropItemContainer.Items.Count != 0)
        {
            PickItemContainer.Items.Add(DropItemContainer.Items[index]);
            DropItemContainer.Items.Remove(DropItemContainer.Items[index]);
        }
        
        
    }
}
