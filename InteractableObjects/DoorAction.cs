using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InteractableObjects
{
    class DoorAction : MonoBehaviour, IInteractable
    {
        public bool isUsable;
        public bool IsUsable { get => isUsable; set => isUsable = value; }

        public void Use()
        {
            GameHandler.Instance.EndAction();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                IsUsable = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                IsUsable = false;
            }
        }
    }
}
