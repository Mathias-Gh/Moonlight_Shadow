using UnityEngine;

namespace TarodevController
{
    public class PlayerAnimator : MonoBehaviour
    {
        private string Attack = "Attack";
        private string Jump = "Jump";

        [Header("References")] [SerializeField]
        private Animator _anim;
        [Header("Settings")]
        [SerializeField, Range(1f, 3f)]
        private float _maxIdleSpeed = 0.5f;
        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;




        private IPlayerController _player;
        private GameObject rotate;
        private bool _grounded;

        private void Awake()
        {
            _player = GetComponentInParent<IPlayerController>();
            rotate = GameObject.Find("character");
        }

        private void OnEnable()
        {
            _player.Jumped += OnJumped;
            
        }

        private void OnDisable()
        {
            _player.Jumped -= OnJumped;
      
        }

        private void Update()
        {
            if (_player == null) return;

            HandleSpriteFlip();

            HandleIdleSpeed();

            HandleCharacterTilt();

            OnAttack();
        }

        private void HandleSpriteFlip()
        {
            if (_player.FrameInput.x != 0)
            {
                rotate.transform.localScale += new Vector3(-1f,1f,1f);
            }
        }

        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(_player.FrameInput.x);
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
        }

        private void HandleCharacterTilt()
        {
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
            _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetTrigger(Jump);
            }
        }

        private void OnAttack()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _anim.SetTrigger(Attack);
            }
        }
        

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");

    }
}