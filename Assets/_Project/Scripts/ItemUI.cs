using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemCountText;
    [SerializeField] private Item _item;
    [SerializeField] private Image _damageIcon;
    
    public Item ItemObj => _item;

    public delegate void OnItemCalled(ItemUI item);
    public event OnItemCalled OnAddFullStack;
    public event Action<ItemUI> OnRemoveLastItem;
    
    
    public void Init(ItemData itemData)
    {
        if (ItemObj.ItemData == null)
        {
            ItemObj.Init(itemData);
            _itemCountText.gameObject.SetActive(true);
            _itemImage.gameObject.SetActive(true);
            _itemCountText.text = _item.ItemCount.ToString();
            _itemImage.sprite = _item.ItemData.ItemImage;
            _damageIcon.gameObject.SetActive(_item.IsDamaged); 
        }
       
    }
    
    public void AddItem(int count)
    {
        
        ItemObj.AddItem(count);
        _itemCountText.text = _item.ItemCount.ToString();
        _itemImage.sprite = _item.ItemData.ItemImage;
        _damageIcon.gameObject.SetActive(_item.IsDamaged); 
    }
    public void RemoveItem(int count)
    {
        ItemObj.RemoveItem(count);
        if(ItemObj.ItemCount <= 0)
        {
            _itemCountText.gameObject.SetActive(false);
            _itemImage.gameObject.SetActive(false);
            OnRemoveLastItem?.Invoke(this);
        }
        else
        {
            _itemCountText.text = _item.ItemCount.ToString();
            _itemImage.sprite = _item.ItemData.ItemImage;
            _damageIcon.gameObject.SetActive(_item.IsDamaged); 
        }
    }
}
