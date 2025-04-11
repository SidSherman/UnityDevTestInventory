using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private List<ItemUI> _items;
    [SerializeField] private int maxCells;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _items = new List<ItemUI>();
        if (_itemPrefab == null)
        {
            Debug.LogError("Item prefab is not assigned in the inspector");
            return;
        }

        if (_itemsContainer == null)
        {
            Debug.LogError("Items container is not assigned in the inspector");
            return;
        }

        for (int i = 0; i < maxCells; i++)
        {
            GameObject _itemUIGM = Instantiate(_itemPrefab, _itemsContainer);
            if (_itemUIGM.TryGetComponent(out ItemUI itemUI))
            {
                _items.Add(itemUI);
                itemUI.OnRemoveLastItem += OnRemoveStack;
            }
        }
    }

    public void AddItem(ItemData item)
    {
        ItemUI freeItem = FindCellToAddItem(item.GetID(), 1);
        if (freeItem != null)
        {
            freeItem.Init(item);
            freeItem.AddItem(1);
        }

    }

    public void OnRemoveStack(ItemUI item)
    {

    }

    public void RemoveItem(ItemData item)
    {
        ItemUI itemUI = FindCellToAddItem(item.GetID());
        if (itemUI != null)
        {

            itemUI.RemoveItem(1);
        }
    }

    public void RemoveItem(int id)
    {
        ItemUI itemUI = FindCellToAddItem(id);
        if (itemUI != null)
        {
            itemUI.RemoveItem(1);
        }
    }

    public ItemUI FindFirstItem(int id)
    {
        foreach (ItemUI item in _items)
        {
            if (item.ItemObj.ItemData != null)
            {
                if (item.ItemObj.GetId() == id)
                {
                    return item;
                }
            }

        }

        return null;
    }

    public ItemUI FindFreeCell()
    {
        foreach (ItemUI item in _items)
        {
            if (item.ItemObj.ItemData == null)
            {

                return item;
            }

        }

        return null;
    }
    /*
    /// <summary>
    /// Return Stackable Item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public ItemUI FindStackableItem(int id, int count)
    {

        foreach (ItemUI item in _items)
        {
            if(item.ItemObj.GetId() == id)
            {
                if (item.ItemObj.CanStack(count))
                {
                    return item;
                }
            }
        }

        return null;
    }*/

    public ItemUI FindCellToAddItem(int id, int count = 0, bool isDamaged = false)
    {
        ItemUI itemWithMinCount = FindFirstItem(id);

        if (itemWithMinCount == null)
        {
            ItemUI freeCell = FindFreeCell();
            if (freeCell)
            {
                return freeCell;
            }
            else
            {
                return null;
            }
        }

        foreach (ItemUI item in _items)
        {
            if (item.ItemObj.ItemData != null)
            {
                if (item.ItemObj.GetId() == id)
                {
                    if (item.ItemObj.CanStack(count))
                    {
                        if (item.ItemObj.ItemData.Type == ItemType.Animal && item.ItemObj.IsDamaged == isDamaged)
                        {
                            if (item.ItemObj.ItemCount < itemWithMinCount.ItemObj.ItemCount)
                            {
                                itemWithMinCount = item;
                            }
                        }
                    }
                }
            }
        }

        if (itemWithMinCount.ItemObj.CanStack(count))
        {
            return itemWithMinCount;
        }
        else
        {
            ItemUI freeCell = FindFreeCell();
            if (freeCell)
            {
                return freeCell;
            }
            else
            {
                return null;
            }
        }
    }
}



