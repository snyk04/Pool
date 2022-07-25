using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool.Balls
{
    public class Ball : IBall
    {
        private readonly GameObject _gameObject;

        public event Action OnDestroy;
        public event Action<GameObject> OnCollision;

        public Ball(GameObject gameObject)
        {
            _gameObject = gameObject;
        }
        
        public void Destroy()
        {
            OnDestroy?.Invoke();
            Object.Destroy(_gameObject);
        }
        
        public void OnCollisionEnter2D(Collision2D col)
        {
            OnCollision?.Invoke(col.gameObject);
        }
    }
}