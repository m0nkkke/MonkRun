using UnityEngine;

public class InfinityRoad : MonoBehaviour
{
    public float speed = 3f; // Скорость движения дороги
    public float roadSegmentLength = 6f; // Длина одного сегмента дороги
    public Transform[] roadSegments; // Массив сегментов дороги

    void Update()
    {
        // Двигаем каждый сегмент дороги
        for (int i = 0; i < roadSegments.Length; i++)
        {
            // Перемещаем сегмент влево
            roadSegments[i].Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            // Если сегмент ушел за пределы, перемещаем его в начало
            if (roadSegments[i].position.x < -roadSegmentLength)
            {
                // Находим самый правый сегмент
                Transform farthestSegment = GetFarthestSegment();
                // Перемещаем текущий сегмент в конец
                Vector3 newPosition = farthestSegment.position + Vector3.right * roadSegmentLength;
                roadSegments[i].position = newPosition;
            }
        }
    }

    // Метод для поиска самого правого сегмента
    private Transform GetFarthestSegment()
    {
        Transform farthestSegment = roadSegments[0];
        for (int i = 1; i < roadSegments.Length; i++)
        {
            if (roadSegments[i].position.x > farthestSegment.position.x)
            {
                farthestSegment = roadSegments[i];
            }
        }
        return farthestSegment;
    }
}