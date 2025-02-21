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
        // Прыжок по пробелу
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Прыжок по пробелу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
            {
                animator.SetTrigger("Jump");
            }
        }

        // Слайд по ПКМ
        if (Input.GetMouseButtonDown(1)) // 1 - это правая кнопка мыши
        {
            if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
            {
                animator.SetTrigger("Slide");
            }
        }
    }
}