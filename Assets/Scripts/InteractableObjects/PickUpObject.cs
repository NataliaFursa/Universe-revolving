using System.Collections;
using UnityEngine;

public class PickUpObject : IInteractable
{
    private float m_moveDistance = 4f;
    private float m_moveSpeed = 5f;
    public override void Interact()
    {
        onPickup?.Invoke(this);
        //player.Pickup(m_part);
    }

    public void Drop()
    {
        MoveRandomly();
    }

    public void MoveRandomly()
    {
        
        if (this.gameObject != null)
        {
            // Генерируем случайный угол в радианах
            float randomAngle = Random.Range(0f, 360f);
            
            // Вычисляем смещение по X и Z
            float xOffset = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * m_moveDistance;
            float zOffset = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * m_moveDistance;

            // Рассчитываем новую целевую позицию, сохраняя значение Y
            Vector3 targetPosition = new Vector3(this.gameObject.transform.position.x + xOffset,
                                                 this.gameObject.transform.position.y,
                                                 this.gameObject.transform.position.z + zOffset);

            // Запускаем корутину для плавного перемещения
            StartCoroutine(MoveToTarget(targetPosition));
        }
        else
        {
            Debug.LogWarning("Объект для перемещения не назначен!");
        }
    }
    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = this.gameObject.transform.position; // Начальная позиция
        float journeyLength = Vector3.Distance(startPosition, targetPosition); // Длина пути
        float startTime = Time.time; // Время начала

        float targetY = targetPosition.y;

        while (Vector3.Distance(this.gameObject.transform.position, targetPosition) > 0.5f)
        {
            // Вычисляем, сколько времени прошло
            float distCovered = (Time.time - startTime) * m_moveSpeed;
            // Находим интерполяцию между начальной и целевой позицией
            float fractionOfJourney = distCovered / journeyLength;

            targetPosition.y = targetY + Mathf.Sin(Mathf.PI * fractionOfJourney) * 3f;

            // Плавно перемещаем объект
            this.gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            yield return null; // Ждем до следующего кадра
        }

        // Устанавливаем объект точно на целевую позицию в конце
        this.gameObject.transform.position = targetPosition;
        Debug.Log("Объект достиг целевой позиции: " + targetPosition);
    }
}
