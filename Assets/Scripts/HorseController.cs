using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public const int MaxLife = 5;
    public static event Action<int> HorseChangeLife = delegate { };
    public static event Action HorseDie = delegate { };
    private int _life = MaxLife;
    private bool _isGrounded = false;

    public GameObject blood;
    public AudioSource hitAudio;

    private Animator _animator;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody2D;
    private bool _isJumping;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        
        GameManager.StatusChanged += GameManagerOnStatusChanged;
    }
    
    private void OnDestroy()
    {
        GameManager.StatusChanged -= GameManagerOnStatusChanged;  
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

    // Use this for initialization
    void Start()
    {
        HorseChangeLife(MaxLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isJumping)
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isJumping && _isGrounded)
        {
            _isJumping = false;
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * 7.5f, ForceMode2D.Impulse);
        //_animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<EnemyRun>().Die();

            _life--;
            HorseChangeLife(_life);

            GameObject _blood = Instantiate(blood, transform);
            _blood.transform.localPosition = Vector2.zero;
            
            hitAudio.Play();
            
            Destroy(_blood, 3f);
        }

        if (_life == 0)
        {
            HorseDie();
        }
    }
}