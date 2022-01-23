using Assets.Scripts.InteractableObjects;
using Assets.Scripts.Items;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameLogic
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] ComponentPanelController gameInterface;
        [SerializeField] ComponentPanelController menu;
        [SerializeField] CheckPanelController losePanel;
        [SerializeField] TimerController timer;
        [SerializeField] Inventory inventory;

        public void EndGameAction(ObjectData data)
        {
            losePanel.Init(data);
        }
        public void PutItem(ItemData item)
        {
            inventory.SpawnItem(item);
        }
        public void SetCurrentItem()
        {
            inventory.InitChoiceItem();
        }
        public void RemoveItem(int id)
        {
            inventory.DeleteItem(id);
        }
        public void Init(bool isPause)
        {
            losePanel.gameObject.SetActive(false);
            inventory.ResetInventory();
            timer.InitTimer();

            if (isPause)
            {
                gameInterface.DisableOnHide();
                menu.OpenPanel();
            }
            else
            {
                gameInterface.OpenPanel();
                menu.DisableOnHide();
            }
        }
    }
}