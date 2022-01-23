using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public class CameraHandler : MonoBehaviour
    {
        public float hight = 15f;
        public float HorizontalFollowSmoothness = 0.15f;
        public float VerticalFollowSmoothness = 0.15f;

        public float OffsetX;
        public float OffsetY;
        public Camera GameCamera => _gameCamera;
        public TargetController Targets => targetController;
        public Transform CameraTransform => _transform;
        public Vector3 WorldToLocalPlanty(Vector3 vector) => Vector3D(vector.x, vector.y, vector.z);
        public Vector3 Vector3ToAxis(Vector3 vector) => Vector3D(vector.x, vector.y, vector.z);
        public float PrevVelocity { get; private set; }
        public Vector3 CameraTargetPosition { get; private set; }
        public Vector3 CameraPosition { get => _transform.position; }
        public Vector2 ScreenSizeInWorldCoordinates { get; private set; }
        public bool IsMovement => _prevCameraPos != _transform.position;
        public bool IsOutTarget {
            get
            {
                Vector2 r = targetController.currentCenter - _transform.position;
                return Vector2D(r.x, r.y).sqrMagnitude > 0.002f;
            }
        }
        public static CameraHandler Instance
        {
            get
            {
                if (Equals(_instance, null))
                {
                    _instance = FindObjectOfType(typeof(CameraHandler)) as CameraHandler;

                    if (Equals(_instance, null))
                        throw new UnityException("ProCamera2D does not exist.");
                }
                return _instance;
            }
        }

        [SerializeField] TargetController targetController;
        [SerializeField] MovementAxis axis;
        [SerializeField] Isometric isometricAxis;
        [SerializeField] bool isCenterOnTargetOnStart;

        private static CameraHandler _instance;
        private Transform _transform;
        private Camera _gameCamera;
        private Vector3 _prevCameraPos;
        List<BaseCameraExtension> _cameraScripts;
        List<Vector3> _influences;

        Func<Vector3, float> AxisX;
        Func<Vector3, float> AxisY;
        Func<Vector3, float> AxisZ;
        Func<float, float, Vector3> Vector2D;
        Func<float, float, float, Vector3> Vector3D;
        Func<Vector3, Vector3> IsometricVector3;

        private List<IPreMove> preMoveActions;
        public void ApplyInfluence(Vector2 influence)
        {
            _influences.Add(Vector2D(influence.x, influence.y));
        }
        private void Awake()
        {
            _transform = transform;
            _prevCameraPos = _transform.position;
            _instance = this;
            _gameCamera = GetComponent<Camera>();
            _influences = new List<Vector3>();
            if (_gameCamera == null)
                Debug.LogError("CameraHandler should be on Camera but it isn't like that");
            ResetAxisFunctions();
        }
        private void Start()
        {
            _cameraScripts = new List<BaseCameraExtension>();
            foreach (BaseCameraExtension component in _transform.GetComponents<BaseCameraExtension>())
                _cameraScripts.Add(component);
            preMoveActions = SortCameraComponents<IPreMove>();

            targetController.Update();
            if (isCenterOnTargetOnStart)
            {
                _transform.position = Vector3D(targetController.currentCenter.x, targetController.currentCenter.y, hight);
            }
            else
            {
                _transform.position = Vector3D(_transform.position.x, _transform.position.y, hight);
            }
        }
        private List<T> SortCameraComponents<T>() where T : ICameraComponent
        {
            List<T> components = new List<T>();
            foreach (object cameraScript in _cameraScripts)
                if (cameraScript is T)
                    components.Add((T)cameraScript);
            return components.OrderBy(a => a.PriorityOrder).ToList();
        }

        private void Update()
        {
            targetController.Update();
            Move();
        }
        private void Move()
        {
            _prevCameraPos = _transform.position;

            foreach (IPreMove preMoveAction in preMoveActions)
                preMoveAction.HandleStartMove(targetController.currentCenter);

            var cameraTargetPositionX = AxisX(targetController.currentCenter);
            var cameraTargetPositionY = AxisY(targetController.currentCenter);
            var cameraTargetPositionZ = AxisZ(targetController.currentCenter) + hight;
            CameraTargetPosition = Vector3D(cameraTargetPositionX, cameraTargetPositionY, cameraTargetPositionZ);

            foreach (Vector3 influnce in _influences)
                CameraTargetPosition += influnce;
            _influences.Clear();

            CameraTargetPosition += Vector2D(OffsetX, OffsetY);

            CameraTargetPosition = IsometricVector3(CameraTargetPosition);

            var horizontalDeltaMovement = Mathf.Lerp(AxisX(_transform.position), AxisX(CameraTargetPosition), HorizontalFollowSmoothness * Time.deltaTime);
            var verticalDeltaMovement = Mathf.Lerp(AxisY(_transform.position), AxisY(CameraTargetPosition), VerticalFollowSmoothness * Time.deltaTime);

            horizontalDeltaMovement -= AxisX(_transform.position);
            verticalDeltaMovement -= AxisY(_transform.position);

            var deltaPosition = Vector3D(horizontalDeltaMovement, verticalDeltaMovement, 0f);

            PrevVelocity = deltaPosition.magnitude;
            _transform.position = _transform.position + deltaPosition;
        }

        public void AddTarget(Transform target)
        {
            targetController.AddTarget(target);
        }
        void ResetAxisFunctions()
        {
            targetController.InitAxis(axis);
            switch (axis)
            {
                case MovementAxis.XY:
                    AxisX = vector => vector.x;
                    AxisY = vector => vector.y;
                    AxisZ = vector => vector.z;
                    Vector2D = (x, y) => new Vector3(x, y, 0);
                    Vector3D = (x, y, z) => new Vector3(x, y, z);
                    break;
                case MovementAxis.XZ:
                    AxisX = vector => vector.x;
                    AxisY = vector => vector.z;
                    AxisZ = vector => vector.y;
                    Vector2D = (x, y) => new Vector3(x, 0, y);
                    Vector3D = (x, y, z) => new Vector3(x, z, y);
                    break;
                case MovementAxis.YZ:
                    AxisX = vector => vector.z;
                    AxisY = vector => vector.y;
                    AxisZ = vector => vector.x;
                    Vector2D = (x, y) => new Vector3(0, y, x);
                    Vector3D = (x, y, z) => new Vector3(z, y, x);
                    break;
            }

            switch (isometricAxis)
            {
                case Isometric.None:
                    IsometricVector3 = (vector) => vector;
                    break;
                case Isometric.Isometric:
                    switch (axis)
                    {
                        case MovementAxis.XZ:
                            IsometricVector3 = (vector) =>
                            {
                                float cameraAngleX = _transform.eulerAngles.x * Mathf.Deg2Rad;
                                float cameraAngleY = _transform.eulerAngles.y * Mathf.Deg2Rad;
                                float cameraAngleZ = _transform.eulerAngles.z * Mathf.Deg2Rad;
                                float tanX = 1f / Mathf.Sin(cameraAngleX);
                                float tanY = Mathf.Tan(cameraAngleY);
                                float cosY = Mathf.Cos(cameraAngleY);
                                Vector3 offset = GameCamera.cameraToWorldMatrix * (new Vector3(0, 0, (-tanX - tanY) * cosY) * hight);
                                return new Vector3(vector.x - offset.x, vector.y, vector.z - offset.z);
                            };
                            break;
                    }
                    break;
            }
        }
    }
}