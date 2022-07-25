using System.Collections.Generic;
using System.Linq;
using Pool.Balls;
using Pool.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pool.UI
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerInputComponent _playerInput;

        [Header("Objects")] 
        [SerializeField] private LineRenderer _playerBallLineRenderer;
        [SerializeField] private LineRenderer _fieldBallLineRenderer;
        [SerializeField] private BallsContainerComponent _ballsContainer;
        
        [Header("Settings")]
        [SerializeField] private int _amountOfTrajectoryPoints;

        private List<Vector3> _playerBallTrajectoryPoints;
        private List<Vector3> _collisionBallTrajectoryPoints;
        private Dictionary<Rigidbody2D, BodyData> _rigidbodiesData;

        private bool _isAiming;

        private void Awake()
        {
            _rigidbodiesData = new Dictionary<Rigidbody2D, BodyData>();
            _playerBallTrajectoryPoints = new List<Vector3>();
            _collisionBallTrajectoryPoints = new List<Vector3>();

            _playerInput.Object.OnAimStart += HandleAimStart;
            _playerInput.Object.OnAimEnd += HandleAimEnd;
        }
        private void Update()
        {
            if (_isAiming)
            {
                DrawTrajectories();
            }
        }

        private void HandleAimStart()
        {
            _isAiming = true;
        }
        private void HandleAimEnd()
        {
            _isAiming = false;
            _playerBallLineRenderer.positionCount = 0;
            _fieldBallLineRenderer.positionCount = 0;
        }
        
        private void DrawTrajectories()
        {
            Rigidbody2D playerBallCopy = CopyBall(_ballsContainer.Object.PlayerBall);
            List<Rigidbody2D> fieldBallsCopies = _ballsContainer.Object.FieldBalls.Select(CopyBall).ToList();

            SaveRigidbodyData(_ballsContainer.Object.PlayerBall);
            _ballsContainer.Object.FieldBalls.ForEach(SaveRigidbodyData);

            DisableObject(_ballsContainer.Object.PlayerBall);
            _ballsContainer.Object.FieldBalls.ForEach(DisableObject);

            bool isPlayerBallDestroyed = false;
            bool isPlayerBallCollisionDetected = false;
            bool isCollisionBallDestroyed = false;
            Rigidbody2D collisionBall = null;
            IBall playerBallComponent = playerBallCopy.GetComponent<BallComponent>().Object;
            playerBallComponent.OnDestroy += () => isPlayerBallDestroyed = true;
            playerBallComponent.OnCollision += obj =>
            {
                if (obj.TryGetComponent(out BallComponent _) && !isPlayerBallCollisionDetected)
                {
                    isPlayerBallCollisionDetected = true;
                    collisionBall = obj.GetComponent<Rigidbody2D>();
                    obj.GetComponent<BallComponent>().Object.OnDestroy += () => isCollisionBallDestroyed = true;
                }
            };
            
            HitBall(playerBallCopy);
            Physics2D.simulationMode = SimulationMode2D.Script;
            SimulatePhysics(_playerBallTrajectoryPoints, _collisionBallTrajectoryPoints, ref playerBallCopy, 
                ref collisionBall, ref isPlayerBallDestroyed, ref isCollisionBallDestroyed);
            
            DestroyObject(playerBallCopy);
            fieldBallsCopies.ForEach(DestroyObject);
            
            EnableObject(_ballsContainer.Object.PlayerBall);
            _ballsContainer.Object.FieldBalls.ForEach(EnableObject);
            
            LoadRigidbodyData(_ballsContainer.Object.PlayerBall);
            _ballsContainer.Object.FieldBalls.ForEach(LoadRigidbodyData);

            ConfigureLineRenderer(_playerBallLineRenderer, _playerBallTrajectoryPoints.ToArray());
            ConfigureLineRenderer(_fieldBallLineRenderer, _collisionBallTrajectoryPoints.ToArray());
            _playerBallTrajectoryPoints.Clear();
            _collisionBallTrajectoryPoints.Clear();
            
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        }
        private Rigidbody2D CopyBall(Rigidbody2D ball)
        {
            Rigidbody2D ballCopy = Instantiate(ball, ball.transform.position, ball.transform.rotation);
            ballCopy.velocity = ball.velocity;
            ballCopy.angularVelocity = ball.angularVelocity;

            return ballCopy;
        }
        private void SaveRigidbodyData(Rigidbody2D rb)
        {
            if (_rigidbodiesData.ContainsKey(rb))
            {
                _rigidbodiesData[rb].Position = rb.transform.position;
                _rigidbodiesData[rb].Rotation = rb.transform.rotation;
                _rigidbodiesData[rb].Velocity = rb.velocity;
                _rigidbodiesData[rb].AngularVelocity = rb.angularVelocity;
            }
            else
            {
                var rigidbodyData = new BodyData()
                {
                    Position = rb.transform.position,
                    Rotation = rb.transform.rotation,
                    Velocity = rb.velocity,
                    AngularVelocity = rb.angularVelocity
                };
                _rigidbodiesData.Add(rb, rigidbodyData);
            }
        }
        private void DisableObject(Component component)
        {
            component.gameObject.SetActive(false);
        }
        private void HitBall(Rigidbody2D ball)
        {
            Vector2 touchPosition = Touchscreen.current.position.ReadValue();
            Vector2 touchPositionInWorldCoordinates = Camera.main.ScreenToWorldPoint(touchPosition);
            Vector2 direction = (ball.position - touchPositionInWorldCoordinates).normalized;
            
            ball.velocity = direction * _playerInput.Object.HitPower;
        }
        private void SimulatePhysics(ICollection<Vector3> playerBallTrajectoryPoints, 
            ICollection<Vector3> collisionBallTrajectoryPoints, ref Rigidbody2D playerBallCopy, 
            ref Rigidbody2D collisionBall, ref bool isPlayerBallDestroyed, ref bool isCollisionBallDestroyed)
        {
            for (int i = 0; i < _amountOfTrajectoryPoints; i++)
            {
                if (!isPlayerBallDestroyed)
                {
                    playerBallTrajectoryPoints.Add(playerBallCopy.transform.position);
                }
                if (collisionBall != null && !isCollisionBallDestroyed)
                {
                    collisionBallTrajectoryPoints.Add(collisionBall.transform.position);
                }
                Physics2D.Simulate(Time.fixedDeltaTime);
            }
        }
        private void DestroyObject(Component component)
        {
            Destroy(component.gameObject);
        }
        private void EnableObject(Component component)
        {
            component.gameObject.SetActive(true);
        }
        private void LoadRigidbodyData(Rigidbody2D rb)
        {
            if (_rigidbodiesData.ContainsKey(rb))
            {
                rb.transform.position = _rigidbodiesData[rb].Position;
                rb.transform.rotation = _rigidbodiesData[rb].Rotation;
                rb.velocity = _rigidbodiesData[rb].Velocity;
                rb.angularVelocity = _rigidbodiesData[rb].AngularVelocity;
            }
        }
        private void ConfigureLineRenderer(LineRenderer lineRenderer, Vector3[] trajectoryPoints)
        {
            lineRenderer.positionCount = trajectoryPoints.Length;
            lineRenderer.SetPositions(trajectoryPoints);
        }

        private class BodyData
        {
            public Vector3 Position { get; set; }
            public Quaternion Rotation { get; set; }
            public Vector2 Velocity { get; set; }
            public float AngularVelocity { get; set; }
        }
    }
}