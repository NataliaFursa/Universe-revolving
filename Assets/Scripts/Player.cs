using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using static PartsDB;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Movement))]

public class Player : MonoBehaviour
{
    enum PlayerState { Frozen, Base, Dash }

    Movement m_movement;
    Dash m_dash;
    Health m_health;
    public Health m_healthValue => m_health;

    MeleePunch m_meleePunch;
    WeaponManager m_weaponManager;
    PlayerStats m_stats;
    public PlayerStats statsValue => m_stats;
    public WeaponManager weaponManagerValue => m_weaponManager;
    PlayerAnimationManager m_animator;
    //SkillManager m_skillManager;
    SkillsManager m_skillsManager;
    public SkillsManager skillsManagerValue => m_skillsManager;
    private PlayerState m_state = PlayerState.Base;
    public Action onDash;
    public Action<Vector3> onMove;
    public Action onPlayerDeath;
    public event Action onPlayerFire;
    public event Action onPlayerSkill1;
    public event Action onPlayerSkill2;

    private Vector3 m_position = new Vector3();
    private Vector3 m_prevFramePosition = new Vector3();


    public void Awake()
    {
        m_movement = GetComponent<Movement>();
        m_dash = GetComponent<Dash>();
        m_health = GetComponent<Health>();
        m_meleePunch = GetComponent<MeleePunch>();
        m_weaponManager = GetComponent<WeaponManager>();
        m_skillsManager = GetComponent<SkillsManager>();
        m_animator = GetComponentInChildren<PlayerAnimationManager>();
        m_animator.player = this;
        m_animator.weapon = GetComponentInChildren<Weapon>();
        m_stats = GetComponent<PlayerStats>();


        m_dash.onDashStart += SetStateToDash;
        m_dash.onDashStart += OnDashStart;
        m_dash.onDashEnd += SetStateToBase;
        m_skillsManager.OnHealingCooldown += Skill1;
        m_skillsManager.OnBoostCooldown += Skill2;

        m_health.onZeroHealth += Die;

        m_prevFramePosition = transform.position;
        //this.ToDefault();
    }

    private void LateUpdate()
    {
        //Debug.Log(m_state);
        m_prevFramePosition = m_position;
        m_position = transform.position;
    }

    public void Move(Vector3 direction, Vector3 basicAngle)
    {
        if (m_state == PlayerState.Base)
        {
            onMove?.Invoke(direction);
            m_movement.Move(direction, basicAngle);
        }
        if (m_state == PlayerState.Dash)
        {
            m_dash.DashMove();
        }
    }
    public void Pickup(IInteractable obj)
    {
        bool availableUse = true;
        if (obj is IItemWithPrice)
        {
            IItemWithPrice item = (IItemWithPrice)obj;
            availableUse = m_stats.MinusMoney(item.Price);
        }
        if (!availableUse) return;

        if (obj is PartPickUpObject)
        {
            var partPickUp = obj as PartPickUpObject;
            m_weaponManager.Pickup(partPickUp.item);
            Destroy(obj.gameObject);
        }
        if (obj is HealPickUp)
        {
            var healPickUp = obj as HealPickUp;
            this.m_health.RestoreHealth(healPickUp.healValue);
            Destroy(obj.gameObject);
        }
        
    }

    public void Warp(Vector3 position)
    {
        m_movement.WarpTo(position);
    }
    public void PlusMoney(int money)
    {
        m_stats.PlusMoney(money);
    }
    public void MinusMoney(int money)
    {
        m_stats.MinusMoney(money);
    }

    public void ToDefault()
    {
        m_stats.ToDefault();
        m_weaponManager.ToDefault();
        m_health.ToDefault(m_stats.baseHP);
        m_movement.ToDefault(m_stats.baseMoveSpeed);
    }
    public void RotateTo(Vector3 target)
    {
        m_movement.RotateToPosition(target);
    }

    public void StartFire()
    {
        m_weaponManager.StartFire();
        onPlayerFire?.Invoke();
    }
    public void StopFire()
    {
        m_weaponManager.StopFire();
    }

    public void Skill1()
    {
        if (!m_skillsManager.IsHealingOnCooldown)
        {
            m_skillsManager.ActivateHealingSkill();
            onPlayerSkill1?.Invoke();
        }
    }

    public void Skill2()
    {
         if (!m_skillsManager.IsBoostOnCooldown)
        {
            m_skillsManager.ActivateBoostSkill();
            onPlayerSkill2?.Invoke();
        }
    }

    public void ExtraAction()
    {
        m_weaponManager.Reload();
    }

    public void Dash()
    {
        if (m_state == PlayerState.Base)
        {
            if (m_dash != null)
            {
                Vector3 dashDir = m_position - m_prevFramePosition;
                if (dashDir == Vector3.zero)
                {
                    return;
                }
                dashDir.y = 0;
                m_dash.StartDash(dashDir.normalized);
            }
        }
    }

    public void Die()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath.Invoke();
        }
    }

    private void OnDashStart()
    {
        if (onDash != null)
        {
            onDash.Invoke();
        }
    }

    private void SetStateToFrozen()
    {
        m_state = PlayerState.Frozen;
    }
    private void SetStateToDash()
    {
        m_state = PlayerState.Dash;
        var test = m_state;
    }
    private void SetStateToBase()
    {
        m_state = PlayerState.Base;
    }
}
