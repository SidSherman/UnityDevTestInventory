using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/Item Data")]

public class ItemData : ScriptableObject
{
    [SerializeField] private int _id;
    
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Sprite _itemImage;
    [SerializeField] private string _itemName;
    [SerializeField] private int _itemStackSize;
    /// <summary>
    ///  only for animals
    /// </summary>
    [HideInInspector]
    [SerializeField] private bool _isDamaged;
    

    public bool IsDamaged
    {
        get => _isDamaged;
        set => _isDamaged = value;
    }
    public int ID => _id;
    
    public ItemType Type
    {
        get => _itemType;
        set => _itemType = value;
    }
    public Sprite ItemImage => _itemImage;
    public string ItemName => _itemName;
    public int ItemStackSize => _itemStackSize;
    
    public ItemType GetItemData()
    {
        return _itemType;
    }
    
    public int GetID()
    {
        return ID;
    }
    
}


[CustomEditor(typeof(ItemData))]
[CanEditMultipleObjects]
public class ItemDataEditor : Editor
{
    
    SerializedProperty itemTypeProp;
    SerializedProperty damageвProp;


    private void OnEnable()
    {
        itemTypeProp = serializedObject.FindProperty("_itemType");
        damageвProp = serializedObject.FindProperty("_isDamaged");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        
        ItemType itemType = (ItemType)itemTypeProp.enumValueIndex;

        if (itemType == ItemType.Animal)
        {
            EditorGUILayout.PropertyField(damageвProp);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
