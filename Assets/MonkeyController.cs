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
        

        SwipeDetection.ClearAllSubscribers();
        SwipeDetection.SwipeEvent += SwipeControl;
    }

    void Update()
    {
        if ( GameManager.Instance.isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameManager.Instance.onRoad)  
                    ToJump();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                    ToSlide();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                // ������������ � Run ����� ���������� ��������
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    PlayAnimation("Running");
                }
            }
        }
    }

    private void PlayAnimation(string animationName)
    {
        animator.Play(animationName, 0, 0);
    }

    private void ToJump()
    {
        PlayAnimation("Jump");
        // ������������ ��� ���������� ��������
        //GameManager.Instance.onRoad = false;
        GameSoundManager.Instance.TriggerJumpSound();
    }

    private void ToSlide()
    {
        PlayAnimation("Slide");
        GameSoundManager.Instance.TriggerCrouchSound();
    }

    private void SwipeControl(Vector2 e)
    {
        if (GameManager.Instance.isRunning) 
        {
            if (e.y > 0)
                if (GameManager.Instance.onRoad)
                    ToJump();
            else if (e.y < 0)
                ToSlide();
        }
        else if (e.x > 0)
        {
            GameManager.Instance.Restart();
        }
    }




    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    //void Update()
    //{
    //    // ������ �� �������
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    // ������ �� �������
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
    //        {
    //            animator.ResetTrigger("Slide");
    //            animator.SetTrigger("Jump");
    //        }
    //    }

    //    // ����� �� ���
    //    if (Input.GetMouseButtonDown(1)) // 1 - ��� ������ ������ ����
    //    {
    //        if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Slide"))
    //        {
    //            animator.ResetTrigger("Jump");
    //            animator.SetTrigger("Slide");
    //        }
    //    }
    //}
}