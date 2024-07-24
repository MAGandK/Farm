using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventory;
    private float _timeInterval = 2f;

    private void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponentInChildren<GroundItem>();

        if (groundItem)
        {
            var itemObject = groundItem.item;

            if (itemObject.isPipen)
            {
                Item invItem = new Item(itemObject);
                _inventory.AddItem(invItem, 1);
                Debug.Log("Item"+ itemObject.Id);
                itemObject.isPipen = false;
            }
            else if (!itemObject.isPipen)
            {
                StartCoroutine(SwitchGrowthStages(groundItem.transform, itemObject));
            }
        }
    }
        private IEnumerator SwitchGrowthStages(Transform itemTransform, ItemObject itemObject)
        {
            int currentStage = 0;

        while (!itemObject.isPipen)
        {
            DeactivateItem(itemTransform, itemObject);
            ActivateItem(itemTransform, itemObject, currentStage);
            currentStage++;
            
            if (currentStage > 3) 
            {
                itemObject.isPipen = true;
                ActivateItem(itemTransform, itemObject, 3); 
                break;
            }
            
            yield return new WaitForSeconds(_timeInterval);
        }
    }

    private void DeactivateItem(Transform itemTransform, ItemObject itemObject)
    {
        for (int i = 0; i <= 3; i++)
        {
            if (i == itemObject.RipenType1 
                || i == itemObject.RipenType2 
                || i == itemObject.RipenType3 
                || i == itemObject.RipenReady)
            {
                itemTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void ActivateItem(Transform itemTransform, ItemObject itemObject, int stageIndex)
    {
        itemTransform.GetChild(stageIndex).gameObject.SetActive(true);
    }
}
