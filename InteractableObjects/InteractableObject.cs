using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InteractableObjects
{
    public interface IInteractable
    {
        bool IsUsable{ get; set; }
        void Use();
    }
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] protected ObjectData objectData;
        protected bool _isUsable;
        public string ObjectName => objectData.objectName;
        public bool IsBreaking { get; protected set; } = false;
        public bool IsInteractable { get; protected set; } = true;
        public void SetBreaking(bool isBreaking)
        {
            if (!isBreaking && objectData.posibleItem.Length > 0)
                IsInteractable = false;
            IsBreaking = isBreaking;
            UpdateColor();
            UpdateModel();
        }
        public void DisableObject()
        {
            IsInteractable = false;
        }
        public bool IsUsable { get => _isUsable; set
            {
                _isUsable = value;
                UpdateColor();
            }
        }
        private void UpdateModel()
        {
            InteractableObject nextStateModel = IsBreaking ? objectData.brokenModel : objectData.fixedModel;
            nextStateModel = Instantiate(nextStateModel, transform.position, nextStateModel.transform.rotation, transform.parent);
            nextStateModel.IsBreaking = IsBreaking;
            nextStateModel.UpdateColor();
            Destroy(gameObject);
        }
        private void UpdateColor()
        {
            if (IsInteractable)
            {
                if (IsUsable)
                {
                    if(objectData.posibleItem.Length > 0)
                        GetComponent<Renderer>().material.color = GameHandler.Instance.ColorOnFixable;
                    else
                        GetComponent<Renderer>().material.color = GameHandler.Instance.ColorOnInteractable;

                }
                else if (IsBreaking)
                    GetComponent<Renderer>().material.color = Color.red;
                else
                    GetComponent<Renderer>().material.color = Color.white;
            }
            else
                GetComponent<Renderer>().material.color = Color.white;
        }

        public abstract void Use();

        public ObjectData ObjectInfo => objectData;
    }
}