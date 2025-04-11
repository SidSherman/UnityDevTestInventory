using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class test : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button addItemButton;
    [SerializeField] private Button removeItemButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private List<ItemData> Items = new List<ItemData>();
    [SerializeField] private InventoryUI _inventoryUI;
    private int currentIndex = 0;

    private void Start()
    {
        UpdateText();

        nextButton.onClick.AddListener(Next);
        previousButton.onClick.AddListener(Previous);
        addItemButton.onClick.AddListener(Add);
        removeItemButton.onClick.AddListener(Remove);
        
    }

    private void Add()
    {
        if (Items[currentIndex])
        {
            _inventoryUI.AddItem(Items[currentIndex]);
        }
        
    }
    private void Remove()
    {
        if (Items[currentIndex])
        {
            _inventoryUI.RemoveItem(Items[currentIndex]);
        }
    }
    
    private void Next()
    {
        currentIndex = (currentIndex + 1) % Items.Count;
        UpdateText();
    }

    private void Previous()
    {
        currentIndex = (currentIndex - 1 + Items.Count) % Items.Count;
        UpdateText();
    }

    private void UpdateText()
    {
        if (Items != null && Items.Count > 0)
            textDisplay.text = Items[currentIndex].name;
    }
}
