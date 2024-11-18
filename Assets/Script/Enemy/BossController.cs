using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    [SerializeField]
    private Animator animatorExplo1;
    [SerializeField]
    private Animator animatorExplo2;
    
    [SpineAnimation][SerializeField]
    private string attackAnimation;
    [SpineAnimation][SerializeField]
    private string attack2Animation;
    [SpineAnimation][SerializeField]
    private string idleAnimation;

    private float _fireRate = 4f;
    [SerializeField]
    private float _fireCooldown; 
    [SerializeField]
    private int _countAtkCombo = 0;
    
    private Vector3 _originalPosition;
    private bool _isReturningToOriginalPosition = false;
    private void Awake()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void Start()
    {
        _skeletonAnimation.AnimationName = idleAnimation;
        _originalPosition = transform.position;
    }

    private void Update()
    {
        HandleAttackCombo();
    }

    private void HandleAttackCombo()
    {
        if (_fireCooldown > 0f)
        {
            _fireCooldown -= Time.deltaTime;
        }
        
        if (_fireCooldown <= 0f && IsCurrentAnimationComplete())
        {
            if (_countAtkCombo < 1)
            {
                Debug.Log("attack1");
                PlayAnimation(attackAnimation, false);
                transform.Translate(.5f * Time.deltaTime, 0, 0);
                _countAtkCombo++;
                ActionManager.OnUpdateUIFollowBoss?.Invoke();
                MoveCharacterUp();
            }
            else
            {
                Debug.Log("attack2");
                PlayAnimation(attack2Animation, false);
                _countAtkCombo = 0;
                ActionManager.OnUpdateUIFollowBoss?.Invoke();
                MoveCharacterUp();
            }
            
            _fireCooldown = _fireRate;
        }
        else if (!IsCurrentAnimationPlaying(idleAnimation) && IsCurrentAnimationComplete())
        {
            if (transform.position.y < _originalPosition.y)
            {
                ActionManager.OnUpdateUIDefaultBoss?.Invoke();
                _isReturningToOriginalPosition = true;
                MoveCharacterDown();
            }
            
            if (!_isReturningToOriginalPosition)
            {
                PlayAnimation(idleAnimation, true);
            }
        }
    }
    
    private void PlayAnimation(string animationName, bool loop)
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
    }
    
    private bool IsCurrentAnimationComplete()
    {
        var currentAnimation = _skeletonAnimation.AnimationState.GetCurrent(0);
        return currentAnimation != null && currentAnimation.IsComplete;
    }
    
    private bool IsCurrentAnimationPlaying(string animationName)
    {
        var currentAnimation = _skeletonAnimation.AnimationState.GetCurrent(0);
        return currentAnimation != null && currentAnimation.Animation.Name == animationName;
    }

    private void MoveCharacterUp()
    {
        transform.position = new Vector3(transform.position.x, _originalPosition.y - 1f, transform.position.z);
    }

    private void MoveCharacterDown()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_originalPosition.x, _originalPosition.y, transform.position.z), Time.deltaTime * 5f);

        if (transform.position.y <= _originalPosition.y + 0.1f)
        {
            transform.position = new Vector3(transform.position.x, _originalPosition.y, transform.position.z);
            _isReturningToOriginalPosition = false;
        }
    }
 }