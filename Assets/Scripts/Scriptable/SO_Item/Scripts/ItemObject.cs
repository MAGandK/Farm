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
    public GameObject Ripe;
    public GameObject RipeType1;
    public GameObject RipeType2;

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

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}