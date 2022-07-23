using System;
using Pool.GameRules;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pool.UI
{
    public class GameEndWindow : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameCycleComponent _gameCycle;
        
        [Header("Objects")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Text _gameResultText;
        [SerializeField] private Button _restartButton;
        
        [Header("Settings")]
        [SerializeField] private string _gameOverText;
        [SerializeField] private string _victoryText;

        private void Awake()
        {
            _gameCycle.Object.OnGameEnd += HandleGameEnd;
            
            _restartButton.onClick.AddListener(() =>
            {
                int currentSceneId = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneId);
            });
        }

        private void HandleGameEnd(GameEndType gameEndType)
        {
            _container.SetActive(true);
            
            string text = gameEndType switch
            {
                GameEndType.GameOver => _gameOverText,
                GameEndType.Victory => _victoryText,
                _ => throw new ArgumentException("This game end type could not be handled.")
            };

            _gameResultText.text = text;
        }
    }
}