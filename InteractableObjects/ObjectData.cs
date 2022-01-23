using Assets.Scripts.Items;
using UnityEngine;

namespace Assets.Scripts.InteractableObjects
{
    public enum InteractiveTypes
    {
        Turnable,
        Openable,
        Crashable,
        Destroyable
    }
    [CreateAssetMenu(fileName = "BreakingObjectPreset", menuName = "ScriptableObjects/BreakingObjectPreset", order = 3)]
    public class ObjectData : ScriptableObject
    {
        public int ID;
        public string objectName;
        public AudioClip useSound;
        public ItemData[] posibleItem;
        public InteractableObject brokenModel;
        public InteractableObject fixedModel;
        public InteractiveTypes interactiveTypes;
    }
}