using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.InteractableObjects
{
    // Change learnLevel for use learn hints or only WrongItem hints
    public enum HintType
    {
        WrongItem,
        Item,
        UsableObject
    }
    [Serializable]
    public struct Hint
    {
        public HintType _hint;
        public GameObject _hintObject;
        public float _hintTime;
    }
    public class HintManager : MonoBehaviour
    {
        [SerializeField]
        private Hint[] _hints;
        private static HintManager _hintManager;

        public static HintManager GetHintManager()
        {
            if (_hintManager == null)
            {
                _hintManager = FindObjectOfType<HintManager>();
            }
            return _hintManager;
        }
        public void ShowHint(Vector3 position, HintType type)
        {
            if (SessionManager.Instance.CurrentLevel == 0)
                ShowHint(position, (int)type);
            else if(type == HintType.WrongItem)
                ShowHint(position, 0);
        }
        public void ShowHint(Transform transform, HintType type)
        {
            ShowHint(transform.position, type);
        }
        private void ShowHint(Vector3 position, int type)
        {
            GameObject hint = Instantiate(_hints[type]._hintObject,
            position + new Vector3(0, 3, 0), _hints[type]._hintObject.transform.rotation);
            Destroy(hint, _hints[type]._hintTime);
        }
    }
}
