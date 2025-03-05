using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartsDB;

public class Weapon : MonoBehaviour
{
    enum State { Idle, Fire, Reload }
    private State m_state = State.Idle;
    public Transform m_muzzle;
    public Projectile bulletPrefab;
    private Coroutine m_fireCoroutine;
    private int ammo;
    private WeaponLego lego;
    public float Damage = 20;


    public Action onShoot;
    public Action onReloadStart;
    public Action onReloadEnd;

    public int Ammo { get => ammo; }
    public int MaxAmmo = 228;
    public WeaponLego weaponLegoValue => lego;
    public LayerMask hitLayer;
    public LineRenderer lineRenderer;

    public void Awake()
    {
        lego = GetComponent<WeaponLego>();
        var partM = lego.magazine.part as Magazine;
        ammo = partM.cage;
        MaxAmmo = partM.cage;
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.5f; // Ширина линии в начале
            lineRenderer.endWidth = 0.5f; // Ширина линии в конце
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Материал для линии
            lineRenderer.startColor = Color.red; // Цвет начала линии
            lineRenderer.endColor = Color.red; // Цвет конца линии
        }
        hitLayer = ~LayerMask.GetMask("Default");
    }

    public void ToDefault()
    {
        lego = GetComponent<WeaponLego>();
        lego.ToDefault();
    }

    public void Pickup(Item item)
    {
        lego.Pickup(item);
        Reload();
    }
    public void GoBase()
    {
        lego.GoBase();
    }

    public void Reload()
    {
        var partM = lego.magazine.part as Magazine;
        if (ammo != partM.cage && (m_state == State.Fire || m_state == State.Idle))
        {
            //Debug.Log($"перезарядка");
            m_state = State.Reload;
            onReloadStart?.Invoke();

            StartCoroutine(ReloadDelay());
            if (m_fireCoroutine != null)
            {
                StopCoroutine(m_fireCoroutine);
                m_fireCoroutine = null;
            }
        }
    }
    private IEnumerator ReloadDelay()
    {
        var partM = lego.magazine.part as Magazine;
        yield return new WaitForSeconds(partM.recharge);
        ammo = partM.cage;
        onReloadEnd?.Invoke();
        m_state = State.Idle;
    }
    public void StartFire()
    {
        if (ammo <= 0 && m_state == State.Idle)
        {
            Reload();
            return;
        }

        if (m_state == State.Idle)
        {
            m_state = State.Fire;
            m_fireCoroutine = StartCoroutine(FireDelay());
        }
    }

    private IEnumerator FireDelay()
    {
        var partR = lego.receiver.part as Receiver;
        do
        {
            Shoot();
            yield return new WaitForSeconds(partR.delay);
        }
        while (true);
    }
    private IEnumerator PostFireDelay()
    {
        var partR = lego.receiver.part as Receiver;
        yield return new WaitForSeconds(partR.delay);
        m_state = State.Idle;
    }

    public void StopFire()
    {
        if (m_fireCoroutine != null)
        {
            StopCoroutine(m_fireCoroutine);
            m_fireCoroutine = null;
        }

        if (m_state == State.Fire)
        {
            StartCoroutine(PostFireDelay());
        }
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            Debug.Log($"SHOOTammo - {ammo}");
            ShootAction();
        }
        BulletCounter();
    }

    public void BulletCounter()
    {
        if (ammo > 0)
        {
            ammo = ammo - 1;
            onShoot?.Invoke();
            Debug.Log($"ammo - {ammo}");
        }
        else
        {
            Debug.Log($"ammo - pusto {ammo}");
        }
    }

    public void ShootAction()
    {
        var partM = lego.magazine.part as Magazine;
        var partR = lego.receiver.part as Receiver;
        var partS = lego.scope.part as Scope;
        if (partR.LegendaryMod == "") ShootBullet(partM, partR, partS);
        if (partR.LegendaryMod == "Ray") ShootRay(partM, partR, partS);

    }

    public void ShootBullet(Magazine partM, Receiver partR, Scope partS)
    {
        var spread = partS.spread;
        if (partR.volume > 1)
        {
            spread = spread * partR.volume * 2;
        }
        for (int i = 1; i <= partR.volume; i++)
        {
            Projectile bullet = Instantiate(bulletPrefab, m_muzzle.position, m_muzzle.rotation);

            Projectile projectileScript = bullet.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.maxDistance = partS.range;
                projectileScript.damage = lego.totalDamage;
            }

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float randomSpreadY = UnityEngine.Random.Range(-spread, spread);
                Vector3 spreadDirection = Quaternion.Euler(0, randomSpreadY, 0) * m_muzzle.forward;
                rb.AddForce(spreadDirection.normalized * partR.force, ForceMode.Impulse);
            }
        }
    }
    public void ShootRay(Magazine partM, Receiver partR, Scope partS)
    {
        // Выпускаем Raycast вперёд от позиции объекта
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, partS.range, hitLayer))
        {
            // Если Raycast попал в объект, выводим информацию
            Debug.Log("Попадание в объект: " + hit.collider.name);
            DrawRay(rayOrigin, hit.point); // Отображаем линию до точки попадания
        }
        else
        {
            Debug.Log("Ничего не найдено на расстоянии " + partS.range);
            DrawRay(rayOrigin, rayOrigin + rayDirection * partS.range); // Отображаем линию на максимальное расстояние
        }
    }

    private void DrawRay(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
