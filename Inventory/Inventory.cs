using Assets.Scripts.GameLogic;
using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryCell _cell;
    private List<InventoryCell> _cells;
    private Transform _transform;
    public void SpawnItem(ItemData item)
    {
        InventoryCell cell = Instantiate(_cell, _transform);
        cell.InitCell(item, _cells.Count == 0);
        _cells.Add(cell);
    }
    public void DeleteItem(int id)
    {
        Destroy(_cells[id].gameObject);
        _cells.RemoveAt(id);
    }
    public void ResetInventory()
    {
        foreach (InventoryCell cell in _cells)
            Destroy(cell.gameObject);
        _cells.Clear();
    }
    public void InitChoiceItem()
    {
        for (int i = 0; i < _cells.Count; i++)
            _cells[i].SetActiveChoiced(i == SessionManager.Instance.CurrentInvPosition);
    }
    private void Awake()
    {
        _transform = transform;
        _cells = new List<InventoryCell>();
    }
    private void Start()
    {
        foreach (Transform cell in _transform)
            Destroy(cell.gameObject);
    }
}
