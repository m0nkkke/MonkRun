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
                if (GameManager.Instance.invertMovement) ToSlide();
                else if (GameManager.Instance.onRoad) ToJump();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (GameManager.Instance.invertMovement && GameManager.Instance.onRoad) ToJump();
                else ToSlide();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                // Âîçâðàùàåìñÿ â Run ïîñëå çàâåðøåíèÿ àíèìàöèè
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    PlayAnimation("Running");
                }
            }
        }
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName, 0, 0);
    }

    private void ToJump()
    {
        PlayAnimation("Jump");
        // ÐÀÑÊÎÌÅÍÒÈÒÜ ÄËß ÎÒÊËÞ×ÅÍÈß ÁÀÍÈÕÎÏÀ
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
            {
                if (GameManager.Instance.invertMovement)
                {
                    ToSlide();
                }
                else if (GameManager.Instance.onRoad) ToJump();
            }
            else if (e.y < 0)
            {
                if (GameManager.Instance.invertMovement && GameManager.Instance.onRoad) ToJump();
                else ToSlide();
            }
        }
    }

}