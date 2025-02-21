using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    private Animator animator;
    private int jumpStateHash = Animator.StringToHash("Base Layer.Jump");
    private int slideStateHash = Animator.StringToHash("Base Layer.Slide");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������ �� �������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ������ �� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
            {
                animator.SetTrigger("Jump");
            }
        }

        // ����� �� ���
        if (Input.GetMouseButtonDown(1)) // 1 - ��� ������ ������ ����
        {
            if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
            {
                animator.SetTrigger("Slide");
            }
        }
    }
}