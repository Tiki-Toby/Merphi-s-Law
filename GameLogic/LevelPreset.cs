using Assets.Scripts.Items;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    [CreateAssetMenu(fileName = "LevelPreset", menuName = "ScriptableObjects/LevelPreset", order = 1)]
    public class LevelPreset : ScriptableObject
    {
        public int lvl;
        public string sceneName;
        public int maxFixes;
        public long timer;
        public string[] breakingObjects;
        public ItemData[] items;

        public bool ContainesObject(string searchObjectName)
        {
            foreach (string breakingObject in breakingObjects)
                if (breakingObject.Equals(searchObjectName))
                    return true;
            return false;
        }
    }
}