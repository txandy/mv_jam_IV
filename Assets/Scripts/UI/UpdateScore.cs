using System;
using TMPro;
using UnityEngine;
using Wall;

namespace UI
{
    public class UpdateScore : MonoBehaviour
    {
        public static event Action<int> ScoreUpdate = delegate(int i) { };
        public TextMeshProUGUI _textMesh;

        private int _score = 0;

        private void Awake()
        {
            DeleteWall.EnemyDeleted += DeleteWallOnEnemyDeleted;
            GameManager.GameStart += GameManagerOnGameStart;
        }

        private void OnDestroy()
        {
            DeleteWall.EnemyDeleted -= DeleteWallOnEnemyDeleted;
            GameManager.GameStart -= GameManagerOnGameStart;
        }

        private void GameManagerOnGameStart()
        {
            _score = 0;
            Render();
        }

        private void Start()
        {
            Render();
        }

        private void DeleteWallOnEnemyDeleted()
        {
            _score++;

            Render();
        }

        private void Render()
        {
            _textMesh.text = String.Format("Score: {0}", _score);

            ScoreUpdate(_score);
        }
    }
}