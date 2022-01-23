using Assets.Scripts.Data;
using Assets.Scripts.Items;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InteractableObjects
{
    public class PickableItem : MonoBehaviour
    {
        private ItemData item;
        private Transform _transform;
        private bool _isUsable;
        public bool IsUsable
        {
            get => _isUsable; protected set
            {
                if (value)
                    GetComponent<Renderer>().material.color = GameHandler.Instance.ColorOnInteractable;
                else
                {
                    HintManager.GetHintManager().ShowHint(transform, HintType.Item);
                    GetComponent<Renderer>().material.color = Color.white;
                }
                _isUsable = value;
            }
        }
        private void Awake()
        {
            _transform = transform;
        }
        public void InitItem(ItemData item)
        {
            this.item = item;
        }
        public void Use()
        {
            GameHandler.Instance.PutItem(item);
            Destroy(gameObject);
        }
        public void FixedUpdate()
        {
            bool isUsable = (_transform.position - GameHandler.Instance.player.position).magnitude < GameHandler.Instance.ItemValidDistance;
            if (isUsable != IsUsable)
                IsUsable = isUsable;
        }
    }
}