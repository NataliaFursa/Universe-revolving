using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class MeleeAttack : MonoBehaviour
{
    private float lastAttackTime;
    private NavMeshAgent m_meshAgent;
    public GameObject hitBox;

    [Header("Attack Settings")]
    [SerializeField] protected float damage;
    [SerializeField]protected float attackCooldown = 2f;

    //public event Action AgentAttack;

    private void Awake()
    {
        m_meshAgent = GetComponent<NavMeshAgent>();
        hitBox.SetActive(false);
    }
    public void Attack(Vector3 targetPosition)
    {
        if (m_meshAgent.remainingDistance <= m_meshAgent.stoppingDistance)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                hitBox.SetActive(true);
                StartCoroutine(DisableHitBoxAfterDelay(0.1f));
                lastAttackTime = Time.time;
            }
        }
    }
    private IEnumerator DisableHitBoxAfterDelay(float delay)
    {
        // Ждем указанное время
        yield return new WaitForSeconds(delay);
        
        // Выключаем HitBox
        hitBox.SetActive(false);
    }
}
