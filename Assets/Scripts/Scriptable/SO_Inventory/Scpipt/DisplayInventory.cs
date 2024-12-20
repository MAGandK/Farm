using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    private MouseItem _mouseItem = new MouseItem();
    
    [SerializeField] private GameObject _inventoryCellPrefab;
    [SerializeField] private InventoryObject _inventory;

    [SerializeField] private int X_Start;//стартовая позиция первой ячейки инвенроря по Х
    [SerializeField] private int Y_Start;//стартовая позиция первой ячейки инвенроря по У
    [SerializeField] private int X_SpaseBatweenItem;// расстояние между ячейками по Х
    [SerializeField] private int Y_SpaseBatweenItem; //расстояние между ячейками по У
    [SerializeField] private int NumberOfColum; // количество желаемых столбцов

    private Dictionary<GameObject, InventorySlot> _itemsDisplayed;

    private void Start()
    {
        CreateSlots();
    }

    private void Update()
    {
        UpdateSlots();
    }

    public void CreateSlots()
    {
        _itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < _inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(_inventoryCellPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            AddEvent(obj,EventTriggerType.PointerEnter, delegate { OnEnter(obj); });//событие, когда указатель мыши входит в область объекта.
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });//указатель мыши выходит из области объекта.
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj); });//когда начинается перетаскивание объекта.
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });//когда перетаскивание объекта завершается.
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDragged(obj); });//когда объект перетаскивается.
            
            _itemsDisplayed.Add(obj, _inventory.Container.Items[i]);
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject,InventorySlot> invSlot in  _itemsDisplayed)
        {
            if (invSlot.Value.ID >= 0)
            {
                invSlot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
                    _inventory.DataBase.GetItem[invSlot.Value.Item.Id].uiDisplay;

                invSlot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1,1);

                invSlot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    invSlot.Value.Amount == 1 ? "" : invSlot.Value.Amount.ToString("n0");
            }
            else
            {
                invSlot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    null;

                invSlot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1,0);

                invSlot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void OnDragged(GameObject o)
    {
        if (_mouseItem.obj !=null)
        {
            _mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    private void OnDragEnd(GameObject obj)
    {
        if (_mouseItem.hoverObj)
        {
            _inventory.MoveItem(_itemsDisplayed[obj], _itemsDisplayed[_mouseItem.hoverObj]);
        }
        else
        {
            _inventory.RemoveItem(_itemsDisplayed[obj].Item);
        }
        
        Destroy(_mouseItem.obj);
        _mouseItem.item = null;
    }

    private void OnDragBegin(GameObject obj)
    {
        var mauseObject = new GameObject();
        var rTransform = mauseObject.AddComponent<RectTransform>();
        rTransform.sizeDelta = new Vector2(50, 50);
        mauseObject.transform.SetParent(transform.parent);

        if (_itemsDisplayed[obj].ID >= 0)
        {
            var image = mauseObject.AddComponent<Image>();
            image.sprite = _inventory.DataBase.GetItem[-_itemsDisplayed[obj].ID].uiDisplay;
            image.raycastTarget = false;
        }

        _mouseItem.obj = mauseObject;
        _mouseItem.item = _itemsDisplayed[obj];
    }

    private void OnExit(GameObject obj)
    {
        _mouseItem.hoverObj = null;
        _mouseItem.hoverObj = null;
    }

    private void OnEnter(GameObject obj)
    {
        _mouseItem.hoverObj = obj;
        if (_itemsDisplayed.ContainsKey(obj))
        {
            _mouseItem.hoverItem = _itemsDisplayed[obj];
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3
        (
            X_Start + (X_SpaseBatweenItem * (i % NumberOfColum)),
            Y_Start + (-Y_SpaseBatweenItem * (i / NumberOfColum)),
            0f
        );
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        var eventTrigger = new EventTrigger.Entry();

        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}

public class MouseItem
{
    public GameObject obj;
    public GameObject hoverObj;
    public InventorySlot hoverItem;
    public InventorySlot item;
}
