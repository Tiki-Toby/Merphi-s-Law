using Assets.Scripts.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float velocity;
        [SerializeField] float acceleration;
        [SerializeField] float velocitySmoothness;
        [SerializeField] float rotateSmoothness;
        IMovementInput inputer;
        public Quaternion TargetRotation { get; private set; }
        public Vector3 PrevVelocity { get; private set; }
        public float CurrentVelocity { get; private set; }
        public Vector3 CurrentVellocityDirection { get; private set; }
        Animator _animator;
        Rigidbody _rigidbody;
        Transform _transform;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            inputer = GetComponent<IMovementInput>();
            if (inputer == null)
                Debug.LogError("Input axis didn't find!");
            _animator = GetComponent<Animator>();
        }
        void Start()
        {
            CurrentVelocity = 0;
            CurrentVellocityDirection = Vector2.zero;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Debug.Log(inputer.HorizontalVelocity());
            PrevVelocity = CurrentVellocityDirection;

            Vector3 acel = new Vector3(inputer.HorizontalVelocity(), 0, inputer.VerticalVelocity());
            float hVelocity = VelocityValue(CurrentVellocityDirection.x, acceleration * acel.x);
            float vVelocity = VelocityValue(CurrentVellocityDirection.z, acceleration * acel.z);

            CurrentVellocityDirection = new Vector3(hVelocity, _rigidbody.velocity.y, vVelocity);
            CurrentVellocityDirection = Vector3.ClampMagnitude(CurrentVellocityDirection, velocity * inputer.SprintImpact());
            CurrentVelocity = CurrentVellocityDirection.magnitude;
            _rigidbody.velocity = Vector3.Lerp(PrevVelocity, CurrentVellocityDirection, velocitySmoothness * Time.deltaTime);

            if(acel != Vector3.zero)
            {
                if (CurrentVellocityDirection.magnitude / velocity < 0.4f)
                    TargetRotation = Quaternion.LookRotation(acel);
                else
                    TargetRotation = Quaternion.LookRotation(CurrentVellocityDirection);
            }

            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, TargetRotation, rotateSmoothness * Time.deltaTime);
            UpdateAnimator();
        }
        private void UpdateAnimator()
        {
            float k = CurrentVelocity / velocity / inputer.SprintImpact();
            _animator.SetFloat("Speed", k);
            if (k > 0.5f)
            {
                bool isRun = inputer.SprintImpact() > 1;
                GameHandler.Instance.audioManager.PlayStepSound(true, isRun);
                _animator.SetBool("Run", isRun);
                _animator.SetBool("Walk", true);
            }
            else
            {
                GameHandler.Instance.audioManager.PlayStepSound(false, false);
                _animator.SetBool("Run", false);
                _animator.SetBool("Walk", false);
            }
        }
        private float VelocityValue(float value, float add)
        {
            if(add == 0)
            {
                if (value > 0.02f || value < -0.02f)
                    return Mathf.Lerp(value, 0f, velocitySmoothness * Time.deltaTime);
                else
                    return 0f;
            }
            else
            {
                value += add * Time.deltaTime;
                value = Mathf.Clamp(value, -velocity * inputer.SprintImpact(), velocity * inputer.SprintImpact());
                return value;
            }
        }
        private void Update()
        {
            Debug.Log("Game - " + SessionManager.Instance.IsGame);
            Debug.Log("Pause - " + SessionManager.Instance.IsPause);
        }
    }
}