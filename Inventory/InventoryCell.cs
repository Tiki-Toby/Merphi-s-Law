using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject activePanel;

    public void InitCell(ItemData item, bool isFirst) 
    {
        image.sprite = item.icon;
        activePanel.SetActive(isFirst);
    }
    public void SetActiveChoiced(bool isActive)
    {
        activePanel.SetActive(isActive);
    }
}
