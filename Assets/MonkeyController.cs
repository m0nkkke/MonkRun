using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    private Animator animator;
    private int jumpStateHash = Animator.StringToHash("Base Layer.Jump");
    private int slideStateHash = Animator.StringToHash("Base Layer.Slide");

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayAnimation("Running");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayAnimation("Slide");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
                 animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            // Возвращаемся в Run после завершения анимации
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                PlayAnimation("Running");
            }
        }
    }

    void PlayAnimation(string animationName)
    {
        animator.Play(animationName, 0, 0);
    }


    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    //void Update()
    //{
    //    // Прыжок по пробелу
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    // Прыжок по пробелу
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
    //        {
    //            animator.ResetTrigger("Slide");
    //            animator.SetTrigger("Jump");
    //        }
    //    }

    //    // Слайд по ПКМ
    //    if (Input.GetMouseButtonDown(1)) // 1 - это правая кнопка мыши
    //    {
    //        if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
    //        {
    //            animator.ResetTrigger("Jump");
    //            animator.SetTrigger("Slide");
    //        }
    //    }
    //}
}