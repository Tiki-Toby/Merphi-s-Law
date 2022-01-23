using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class SessionManager : MonoBehaviour
    {
        private static SessionManager _instance;
        public static SessionManager Instance => _instance;
        public float Timer { get; private set; }
        public int CurrentInvPosition { get; private set; }
        public int CurrentLevel { get; private set; }
        public List<ItemData> Inventory { get; private set; }
        public ItemData CurrentItem => Inventory[CurrentInvPosition];
        public bool IsGame { get; private set; }
        public bool IsPause { get; private set; }
        public bool IsGameProcess => IsGame && !IsPause;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }
        public void InitSession(LevelPreset levelPreset)
        {
            IsGame = true;
            IsPause = false;
            CurrentInvPosition = 0;
            Inventory = new List<ItemData>();
            CurrentLevel = levelPreset.lvl;
            Timer = (float)levelPreset.timer;
            if (Timer > 0f)
                StartCoroutine(TimerCoroutine());
        }
        public void SetCurrentItem(int i)
        {
            CurrentInvPosition = i;
        }
        public void AddNextItem(int i)
        {
            CurrentInvPosition += i;
            if(Inventory.Count > 0)
                if (CurrentInvPosition >= Inventory.Count || CurrentInvPosition < 0)
                    CurrentInvPosition = (CurrentInvPosition + Inventory.Count) % Inventory.Count;
        }
        public void PutItem(ItemData item)
        {
            Inventory.Add(item);
        }
        public void RemoveItem(ItemData item)
        {
            Inventory.Remove(item);
            if(CurrentInvPosition == Inventory.Count)
                CurrentInvPosition = Inventory.Count - 1;
        }
        public void SetPause(bool isPause)
        {
            IsPause = isPause;
        }
        public void SwitchPause()
        {
            IsPause = !IsPause;
        }
        public void NextLevel() => CurrentLevel++;
        public void FirstLevel() => CurrentLevel = 0;
        public void EndAction()
        {
            IsGame = false;
            IsPause = true;
        }
        private IEnumerator TimerCoroutine()
        {
            while(Timer > 0f)
            {
                if(!IsPause)
                    Timer -= Time.deltaTime;
                yield return null;
            }
            if (IsGame)
                GameHandler.Instance.EndAction();
        }
    }
}