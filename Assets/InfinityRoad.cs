using UnityEngine;

public class InfinityRoad : MonoBehaviour
{
    public float speed = 3f; // �������� �������� ������
    public float roadSegmentLength = 6f; // ����� ������ �������� ������
    public Transform[] roadSegments; // ������ ��������� ������

    void Update()
    {
        // ������� ������ ������� ������
        for (int i = 0; i < roadSegments.Length; i++)
        {
            // ���������� ������� �����
            roadSegments[i].Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            // ���� ������� ���� �� �������, ���������� ��� � ������
            if (roadSegments[i].position.x < -roadSegmentLength)
            {
                // ������� ����� ������ �������
                Transform farthestSegment = GetFarthestSegment();
                // ���������� ������� ������� � �����
                Vector3 newPosition = farthestSegment.position + Vector3.right * roadSegmentLength;
                roadSegments[i].position = newPosition;
            }
        }
    }

    // ����� ��� ������ ������ ������� ��������
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