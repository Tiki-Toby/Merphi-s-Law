using Assets.Scripts.Data;
using Assets.Scripts.Items;
using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _itemManager;
    private Hashtable _itemsTable;
    public static ItemManager GetItemManager()
    {
        if(_itemManager == null)
            _itemManager = FindObjectOfType<ItemManager>();
        return _itemManager;
    }
    public ItemData GetItemById(int id) {
        if (_itemsTable.ContainsKey(id))
            return (ItemData)_itemsTable[id];
        return null;
    } 
    private void Awake()
    {
        _itemsTable = new Hashtable();
        ItemData[]items = Resources.LoadAll<ItemData>(Paths.Items);
        for (int i = 0; i < items.Length; i++)
            _itemsTable.Add(items[i].ID, items[i]);
    }
}
