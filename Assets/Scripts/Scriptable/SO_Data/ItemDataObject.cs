using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Inventory System/DataBase")]
public class ItemDataObject : ScriptableObject,ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    internal List<ItemObject> GetItem;
    
    public void OnBeforeSerialize()
    {
        GetItem = new List<ItemObject>();
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(Items[i]);
        }
    }
}
