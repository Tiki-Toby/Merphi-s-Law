using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class CanvasStaticChecker : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<Canvas>().Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(FindObjectOfType<Canvas>());
        }
    }
}
