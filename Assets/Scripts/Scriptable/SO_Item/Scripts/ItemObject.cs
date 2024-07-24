using UnityEngine;

public enum ItemType
{
    Fruit,
    Vegetable,
    RootCrop,
    Cereals,
    Tools
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15, 20)] public string description;
    
    public int RipenType1 = 0;
    public int RipenType2 = 1;
    public int RipenType3 = 2;
    public int RipenReady = 3;
    
    public bool isPipen;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    
    public int RipenType1;
    public int RipenType2;
    public int RipenType3;
    public int RipenReady;

    
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;

        RipenType1 = item.RipenType1;
        RipenType2 = item.RipenType2;
        RipenType3 = item.RipenType3;
        RipenReady = item.RipenReady;
    }
}