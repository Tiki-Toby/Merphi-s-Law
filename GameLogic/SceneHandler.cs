using Assets.Scripts.Data;
using Assets.Scripts.InteractableObjects;
using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] LayerMask mask;
        [SerializeField] Transform player;
        [SerializeField] List<Transform> posibleItemSpawnPoints;
        private Vector3 startPlayerPosition;
        private void Awake()
        {
            startPlayerPosition = player.position;
        }
        public void Init(LevelPreset preset)
        {
            List<PickableItem> tmpItems = new List<PickableItem>(FindObjectsOfType<PickableItem>());
            for (int i = 0; i < tmpItems.Count; i++)
                Destroy(tmpItems[i].gameObject);
            int count = posibleItemSpawnPoints.Count;
            foreach (ItemData item in preset.items)
            {
                int i = Random.Range(0, count);
                PickableItem pickedItem = Instantiate(item.prefab, posibleItemSpawnPoints[i].position, Quaternion.identity).GetComponent<PickableItem>();
                pickedItem.InitItem(item);
                count--;
                Transform tmp = posibleItemSpawnPoints[i];
                posibleItemSpawnPoints[i] = posibleItemSpawnPoints[count];
                posibleItemSpawnPoints[count] = tmp;
            }

            InteractableObject[] interactableObjects = FindObjectsOfType<InteractableObject>();
            count = interactableObjects.Length;
            for (int i = 0; i < count; i++)
            {
                if (!preset.ContainesObject(interactableObjects[i].ObjectName))
                {
                    count--;
                    interactableObjects[i].DisableObject();
                    InteractableObject tmp = interactableObjects[i];
                    interactableObjects[i] = interactableObjects[count];
                    interactableObjects[count] = tmp;
                    i--;
                }
            }
            for (int i = preset.maxFixes; i > 0 && count > 0; i--)
            {
                int id = Random.Range(0, count);
                interactableObjects[id].SetBreaking(true);
                count--;
                if (count > -1)
                {
                    InteractableObject tmp = interactableObjects[id];
                    interactableObjects[id] = interactableObjects[count];
                    interactableObjects[count] = tmp;
                }
            }
            for (int i = 0; i < count; i++)
                interactableObjects[i].SetBreaking(false);
            player.position = startPlayerPosition;
        }
        public ObjectData BreakingObject()
        {
            foreach (InteractableObject intObject in FindObjectsOfType<InteractableObject>())
                if (intObject.IsBreaking)
                    return intObject.ObjectInfo;
            return null;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    PickableItem item = hit.transform.GetComponent<PickableItem>();
                    if (item != null)
                        if (item.IsUsable)
                        {
                            if (SessionManager.Instance.Inventory.Count < 9)
                                item.Use();
                            else
                                HintManager.GetHintManager().ShowHint(hit.transform, HintType.WrongItem);
                            return;
                        }
                    IInteractable intObject = hit.transform.GetComponent<IInteractable>();
                    if (intObject != null)
                        if (intObject.IsUsable)
                            intObject.Use();
                }
            }
        }
    }
}