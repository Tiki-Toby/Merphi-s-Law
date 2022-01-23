using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameLogic
{
    class TmpSceneManager
    {

    }
    //{
    //    [SerializeField] Transform player;
    //    [SerializeField] List<Transform> posibleItemSpawnPoints;
    //    private Vector3 startPlayerPosition;
    //    private List<PickableItem> items;
    //    private List<InteractableObject> interactableObjects;
    //    private void Awake()
    //    {
    //        startPlayerPosition = player.position;
    //        items = new List<PickableItem>();
    //    }
    //    public void Init(LevelPreset preset)
    //    {
    //        interactableObjects = new List<InteractableObject>(FindObjectsOfType<InteractableObject>());
    //        PickableItem[] tmpItems = FindObjectsOfType<PickableItem>();
    //        for (int i = 0; i < tmpItems.Length; i++)
    //            Destroy(tmpItems[i].gameObject);
    //        int count = posibleItemSpawnPoints.Count;
    //        items = new List<PickableItem>();
    //        foreach (ItemData item in preset.items)
    //        {
    //            int i = Random.Range(0, count);
    //            PickableItem pickedItem = Instantiate(item.prefab, posibleItemSpawnPoints[i].position, Quaternion.identity).GetComponent<PickableItem>();
    //            pickedItem.InitItem(item);
    //            items.Add(pickedItem);
    //            count--;
    //            Transform tmp = posibleItemSpawnPoints[i];
    //            posibleItemSpawnPoints[i] = posibleItemSpawnPoints[count];
    //            posibleItemSpawnPoints[count] = tmp;
    //        }
    //        count = interactableObjects.Count;
    //        for (int i = 0; i < count; i++)
    //        {
    //            if (!preset.ContainesObject(interactableObjects[i].ObjectName))
    //            {
    //                count--;
    //                interactableObjects[i].DisableObject();
    //                InteractableObject tmp = interactableObjects[i];
    //                interactableObjects[i] = interactableObjects[count];
    //                interactableObjects[count] = tmp;
    //                i--;
    //            }
    //        }
    //        for (int i = preset.maxFixes; i > 0 && count > 0; i--)
    //        {
    //            int id = Random.Range(0, count);
    //            interactableObjects[id].SetBreaking(true);
    //            count--;
    //            if (count > -1)
    //            {
    //                InteractableObject tmp = interactableObjects[id];
    //                interactableObjects[id] = interactableObjects[count];
    //                interactableObjects[count] = tmp;
    //            }
    //        }
    //        for (int i = 0; i < count; i++)
    //            interactableObjects[i].SetBreaking(false);
    //        player.position = startPlayerPosition;
    //    }
    //    public ObjectData BreakingObject()
    //    {
    //        foreach (InteractableObject intObject in interactableObjects)
    //            if (intObject.IsBreaking)
    //                return intObject.ObjectInfo;
    //        return null;
    //    }
    //    public void AddInteractableObject(InteractableObject intObject)
    //    {
    //        interactableObjects.Add(intObject);
    //    }
    //    public void ReplaceInteractableObject(InteractableObject oldObject, InteractableObject newObject)
    //    {
    //        int i = interactableObjects.IndexOf(oldObject);
    //        if (i > -1)
    //            interactableObjects[i] = newObject;
    //        else
    //            interactableObjects.Add(newObject);
    //    }
    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;
    //            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //            {
    //                PickableItem item = hit.transform.GetComponent<PickableItem>();
    //                if (item != null)
    //                    if (item.IsUsable)
    //                    {
    //                        if (SessionManager.Instance.Inventory.Count < 9)
    //                            item.Use();
    //                        else
    //                            HintManager.GetHintManager().ShowHint(hit.transform, HintType.WrongItem);
    //                        return;
    //                    }
    //                InteractableObject intObject = hit.transform.GetComponent<InteractableObject>();
    //                if (intObject != null)
    //                    if (intObject.IsUsable)
    //                    {
    //                        Debug.Log(intObject.name);
    //                        intObject.Use();
    //                    }
    //            }
    //        }
    //    }
    //}
}
