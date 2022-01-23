using Assets.Scripts.GameLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class MovementHandler : MonoBehaviour, IMovementInput
    {
        [SerializeField] float timeForSprint = 5f;
        [SerializeField] float restoreSpeed = 1f;
        [SerializeField] float sprintImpact = 2;
        private float currentSprintTime;
        private float currentSprintImpact;

        public float AgilityPercent => currentSprintTime / timeForSprint;
        private void Awake()
        {
            currentSprintImpact = 1;
            currentSprintTime = timeForSprint;
            StartCoroutine(SprintHandler());
        }
        public float HorizontalVelocity()
        {
            if (SessionManager.Instance.IsGameProcess)
            {
                if (Input.GetKey(KeyCode.D))
                    return 1f * currentSprintImpact;
                else if (Input.GetKey(KeyCode.A))
                    return -1f * currentSprintImpact;
            }
            return 0f;
        }

        public float VerticalVelocity()
        {
            if (SessionManager.Instance.IsGameProcess)
            {
                if (Input.GetKey(KeyCode.W))
                    return 1f * currentSprintImpact;
                else if (Input.GetKey(KeyCode.S))
                    return -1f * currentSprintImpact;
            }
            return 0f;
        }
        private IEnumerator SprintHandler()
        {
            while (this)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
                currentSprintImpact = 2f;
                while (Input.GetKey(KeyCode.LeftShift) && currentSprintTime > 0)
                {
                    currentSprintTime -= Time.deltaTime;
                    yield return null;
                }
                currentSprintImpact = 1f;
                while(currentSprintTime < timeForSprint)
                {
                    currentSprintTime += Time.deltaTime * restoreSpeed;
                    currentSprintTime = Mathf.Clamp(currentSprintTime, 0, timeForSprint);
                    yield return null;
                }
            }
        }

        public float SprintImpact()
        {
            return currentSprintImpact;
        }
    }
}