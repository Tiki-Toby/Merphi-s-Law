using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class AgilityBar : MonoBehaviour
    {
        [SerializeField] Image bar;

        public void Update()
        {
            bar.fillAmount = GameHandler.Instance.player.GetComponent<MovementHandler>().AgilityPercent;
        }
    }
}
