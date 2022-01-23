using Assets.Scripts.Data;
using Assets.Scripts.GameLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] Text timer;
        private void Awake()
        {
            if (timer == null)
                timer = GetComponentInChildren<Text>();
        }
        public void InitTimer()
        {
            if (SessionManager.Instance.Timer > 0)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
        private void Update()
        {
            if (SessionManager.Instance.Timer > 0)
                timer.text = SessionManager.Instance.Timer.ToString("0");
        }
    }
}