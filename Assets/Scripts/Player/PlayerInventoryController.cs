using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventory;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        
        if (item)
        {
            Item invItem = new Item(item.item);
            _inventory.AddItem(invItem,1);
            Debug.Log(invItem.Id);
            Destroy(other.gameObject);
        }
    }
}
