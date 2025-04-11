using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    // only for animals
    private bool _isDamaged;
    public ItemData ItemData => _itemData;
    public int ItemCount { get; set; }

    public bool IsDamaged
    {
        get => _isDamaged;
        set => _isDamaged = value;
    }

    public void Init(ItemData itemData)
    {
        _itemData = itemData;
       
    }
    
    /// <summary>
    /// Returns extra items that cannot be stacked
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddItem(int count)
    {
        if(_itemData == null)
        {
            Debug.LogError("ItemData is null");
            return 0;
        }
        if (CanStack(count))
        {
            ItemCount += count;
            return 0;
        }
        else
        {
            int freeSize = GetFreeSize(count);
            ItemCount += freeSize;
            Debug.LogError($"You try to add more items than you can stack. Was added {freeSize}, remained{count - freeSize}");
            return count - freeSize;
        }

    }
    /// <summary>
    /// Returns positive value of remaining items or negative value of extra items that cannot be removed 
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public int RemoveItem(int count)
    {
        if(ItemCount < count)
        {
            int originCount = ItemCount;
            ItemCount = 0;
            Debug.LogError($"You try to remove more items than contains stack. Was removed {ItemCount }, remained{count - originCount}");
            _itemData = null;
            return originCount - count;
        }
        else
        {
            ItemCount -= count;
            if(ItemCount == 0)
                _itemData = null;
            return ItemCount;
        }
    }

    public void ClearData()
    {
        _itemData = null;
    }
    public bool CanStack(int count)
    {
        return ItemCount + count <= ItemData.ItemStackSize;
    }
    
    public int GetFreeSize(int count)
    {
        return  ItemData.ItemStackSize - ItemCount;
    }
    
    public int GetId()
    {
        return ItemData.ID;
    }
}
