using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyRun : MonoBehaviour
    {
        public static event Action EnemyResearchRespawn = delegate { };
        private Rigidbody2D _rigidbody2D;
        private AudioSource _audioSource;
        private bool _isDead;

        private float _speed = 4f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();

            GameManager.StatusChanged += GameManagerOnStatusChanged;

            int currentLevel = GameManager.Instance.level;
            
            _speed = _speed + (currentLevel * 0.5f);            
        }

        private void OnDestroy()
        {
            GameManager.StatusChanged -= GameManagerOnStatusChanged;
           
        }

        private void Start()
        {
            if (GameManager.Instance.gameStatus == GameManager.Status.Runing)
            {
                _audioSource.Play();
                _audioSource.mute = false;
            }
            else
            {
                _audioSource.mute = true;
            }
        }

        private void GameManagerOnStatusChanged(GameManager.Status s)
        {
            switch (s)
            {
                case GameManager.Status.Pause:
                    _audioSource.mute = true;
                    break;

                case GameManager.Status.Runing:
                    _audioSource.mute = false;
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Respawn"))
            {
                EnemyResearchRespawn();
            }
        }

        private void FixedUpdate()
        {
            if (_isDead) return;

            _rigidbody2D.velocity = Vector2.left * _speed;
        }

        public void Die()
        {
            _isDead = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

            GetComponent<CapsuleCollider2D>().enabled = false;

            float force = Random.Range(7f, 15f);
            _rigidbody2D.AddForce(Vector2.one * force, ForceMode2D.Impulse);

            Destroy(gameObject, 2f);
        }
    }
}