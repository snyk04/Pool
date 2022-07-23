using System;
using System.Collections;
using UnityEngine;

namespace Pool.Common
{
    public abstract class Component<T> : MonoBehaviour
    {
        private T _object;
        public T Object => _object ??= CreateObject();

        private void Awake()
        {
            _object ??= CreateObject();
        }

        protected abstract T CreateObject();

        private void OnDestroy()
        {
            if (((IList)typeof(T).GetInterfaces()).Contains(typeof(IDisposable)))
            {
                ((IDisposable) _object).Dispose();
            }
        }
    }
}