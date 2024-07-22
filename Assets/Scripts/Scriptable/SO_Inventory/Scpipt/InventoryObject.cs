using UnityEngine;

[CreateAssetMenu(fileName = "Inventiry", menuName = "Inventory System/Inventiry")]
public class InventoryObject : ScriptableObject
{
    public Inventory Container;

    public ItemDataObject DataBase;
    
    public void MoveItem(InventorySlot itemFirst, InventorySlot itemSecond)
    {
        InventorySlot temp = new InventorySlot(itemSecond.ID, itemSecond.Item, itemSecond.Amount);
        itemSecond.UpdateSlot(itemFirst.ID, itemFirst.Item, itemFirst.Amount); 
        itemFirst.UpdateSlot(temp.ID, temp.Item, temp.Amount);
    }
    
    public void RemoveItem(Item item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].Item == item)
            {
                Container.Items[i].UpdateSlot(-1,null,0);
            }
        }
    }

    public void AddItem(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == item.Id)
            {
                Container.Items[i].AddAmount(amount);
                return;
            }
        }

        SetEmptySlot(item, amount);
    }

    public InventorySlot SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(item.Id, item, amount);
                return Container.Items[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[28];
}

[System.Serializable]
public class InventorySlot
{ 
    public int ID = -1;
   public Item Item;
   public int Amount;

    public InventorySlot()
    {
        ID = -1;
        Item = null;
        Amount = 0;
    }

    public InventorySlot(int id, Item item, int amount)
    {
        ID = id;
        Item = item;
        Amount = amount;
    }

    public void UpdateSlot(int id, Item item, int amount)
    {
        ID = id;
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int value)
    {
        Amount += value;
    }
}
