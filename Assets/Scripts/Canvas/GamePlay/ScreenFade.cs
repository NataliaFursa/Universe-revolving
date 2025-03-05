using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [Header("Note: this time is counted in frames")]
    [SerializeField] int fadeInTime = 10;
    [SerializeField] int fadeOutTime = 10;

    public Action onFadeOutStart;
    public Action onFadeOutEnd;
    public Action onFadeInEnd;

    private Coroutine m_fadeCoroutine;
    private Image m_blackImage;

    private void Awake()
    {
        m_blackImage = GetComponentInChildren<Image>();
    }

    public void StartFadeSequence()
    {
        ClearCoroutine();
        m_fadeCoroutine = StartCoroutine(FadeSequenceCoroutine());
    }

    public void StartFadeIn()
    {
        ClearCoroutine();
        m_fadeCoroutine = StartCoroutine(FadeInCoroutine());
    }
    private void ClearCoroutine()
    {
        if (m_fadeCoroutine != null)
        {
            StopCoroutine(m_fadeCoroutine);
        }
    }

    public IEnumerator FadeSequenceCoroutine()
    {
        float fadeOutStep = 1 / (float)fadeOutTime;
        if (onFadeOutStart != null)
        {
            onFadeOutStart.Invoke();
        }
        while(m_blackImage.color.a < 1)
        {
            float curOpacity = m_blackImage.color.a;
            curOpacity += fadeOutStep;
            Color color = m_blackImage.color;
            color.a = curOpacity;
            m_blackImage.color = color;
            //Debug.Log(m_blackImage.color);
            yield return new WaitForEndOfFrame();
        }
        float fadeInStep = 1 / (float)fadeInTime;
        if (onFadeOutEnd != null) {
            onFadeOutEnd.Invoke();
        }
        while (m_blackImage.color.a > 0)
        {
            float curOpacity = m_blackImage.color.a;
            curOpacity -= fadeInStep;
            Color color = m_blackImage.color;
            color.a = curOpacity;
            m_blackImage.color = color;
            //Debug.Log(m_blackImage.color);
            yield return new WaitForEndOfFrame();
        }
        if (onFadeInEnd != null)
        {
            onFadeInEnd.Invoke();
        }
    }

    public IEnumerator FadeInCoroutine()
    {
        Color color = m_blackImage.color;
        color.a = 1;
        m_blackImage.color = color;
        float fadeInStep = 1 / (float)fadeInTime;

        while (m_blackImage.color.a > 0)
        {
            float curOpacity = m_blackImage.color.a;
            curOpacity -= fadeInStep;
            color = m_blackImage.color;
            color.a = curOpacity;
            m_blackImage.color = color;
            yield return new WaitForEndOfFrame();
        }
    }
}
