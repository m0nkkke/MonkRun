using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������ �� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        // ����� �� ��� + Shift
        if (Input.GetMouseButtonDown(1)) // 1 - ��� ������ ������ ����
        {
            animator.SetTrigger("Slide");
        }
    }
}