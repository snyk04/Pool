using System;
using System.Globalization;
using Pool.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Pool.UI
{
    public class HitForceTracker : MonoBehaviour
    {
        private const float DefaultHitForceValue = 0;
        
        [Header("References")]
        [SerializeField] private PlayerInputComponent _playerInput;

        [Header("Objects")] 
        [SerializeField] private Text _text;
        [SerializeField] private Slider _slider;

        private bool _isAiming;
        
        private void Awake()
        {
            _playerInput.Object.OnAimStart += () => _isAiming = true;
            _playerInput.Object.OnAimEnd += () => _isAiming = false;
        }
        private void Update()
        {
            float hitForcePercent = _isAiming 
                ? _playerInput.Object.HitPower / _playerInput.Object.MaxHitPower * 100 
                : DefaultHitForceValue;
            
            UpdateInterface(hitForcePercent);
        }

        private void UpdateInterface(float hitForcePercent)
        {
            _text.text = $"{Math.Round(hitForcePercent, 1).ToString(CultureInfo.InvariantCulture)}%";
            _slider.value = hitForcePercent;
        }
    }
}