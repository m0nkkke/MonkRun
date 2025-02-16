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
        // Прыжок по пробелу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        // Слайд по ПКМ + Shift
        if (Input.GetMouseButtonDown(1)) // 1 - это правая кнопка мыши
        {
            animator.SetTrigger("Slide");
        }
    }
}