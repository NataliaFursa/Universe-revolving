using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(CharacterController))]
public class Dash : MonoBehaviour
{
    [Header("Dash settings")]
    [SerializeField] public float dashSpeed = 20;
    [SerializeField] public float dashTime = 2;
    [SerializeField] public float dashCooldown = 1;

    private CharacterController m_charController;
    public event Action onDashStart;
    public event Action onDashEnd;

    private Coroutine m_dashCoroutine;
    private Coroutine m_dashDelay;
    private Vector3 m_dashDirection;

    private void Awake()
    {
        m_charController = GetComponent<CharacterController>();
    }

    public void DashMove()
    {
        m_charController.SimpleMove(m_dashDirection * dashSpeed);
    }

    public void StartDash(Vector3 direction)
    {
         if(m_dashDelay != null)
        {
            Debug.Log("Dash on cooldown");
            return;
        }
        m_charController.excludeLayers = LayerMask.GetMask("ProjectileEnemy");
        m_dashDirection = direction;
        if (onDashStart != null)
        {
            onDashStart.Invoke();
        }
        m_dashCoroutine = StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        if (onDashEnd != null)
        {
            onDashEnd.Invoke();
        }
        m_dashDirection = new Vector3();
        m_charController.excludeLayers = new LayerMask();
        m_dashDelay = StartCoroutine(CooldownCoroutine());
        Debug.Log("Dash ended");
    }
    
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        m_dashDelay = null;
    }
}
