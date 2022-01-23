using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "ItemPreset", menuName = "ScriptableObjects/ItemPreset", order = 2)]
    public class ItemData : ScriptableObject
    {
        public int ID;
        public string localizeName;
        public Sprite icon;
        public GameObject prefab;
    }
}