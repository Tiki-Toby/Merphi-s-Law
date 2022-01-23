using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class PanelActiveSwitch : PanelSpawnController
    {
        public override void HidePanel()
        {
            base.HidePanel();
            gameObject.SetActive(false);
        }
        public override void OpenPanel()
        {
            base.OpenPanel();
            gameObject.SetActive(true);
        }
    }
}
