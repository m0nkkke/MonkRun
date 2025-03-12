using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RockCollider")
        {
            print("Rock");
            GameSoundManager.Instance.TriggerHitSound();
            collision.collider.enabled = false;
            //GameManager.Instance.roadSpeed = 0;
            animator.Play("Hit On Legs", -1, 0f);

            GameManager.Instance.OnMonkCollision(ColliderTypes.Rock);
            //GameManager.Instance.ResetGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        bool flagDestroy = false;
        switch (tag)
        {
            case "Banana":
                GameManager.Instance.Bananas += 1 * GameManager.Instance.CoefBanana;
                GameSoundManager.Instance.TriggerBananaSound();
                flagDestroy = true;
                break;
            case "MushroomDN":
                flagDestroy = true;
                GameSoundManager.Instance.TriggerMushroomSound();
                if (GameManager.Instance.mushroomDNCoroutine != null)
                {
                    StopCoroutine(GameManager.Instance.mushroomDNCoroutine);
                    resetDN();
                }
                GameManager.Instance.mushroomDNCoroutine = StartCoroutine(SwipeDayNight());
                break;
            case "MushroomB":
                flagDestroy = true;
                GameSoundManager.Instance.TriggerMushroomSound();
                StartCoroutine(IncreaseBananaCoef());
                break;
            case "MushroomMB":
                GameSoundManager.Instance.TriggerMushroomSound();
                flagDestroy = true;
                ReduceBananas();
                break;
            case "MushroomS":
                GameSoundManager.Instance.TriggerMushroomSound();
                flagDestroy = true;
                if (GameManager.Instance.mushroomSpeedCoroutine != null)
                {
                    StopCoroutine(GameManager.Instance.mushroomSpeedCoroutine);
                    resetS();
                }
                GameManager.Instance.mushroomSpeedCoroutine = StartCoroutine(ReduceSpeedTemporarily());
                break;
        }
        if (flagDestroy) Destroy(other.gameObject);
    }
    private int diffSpeed = 0;
    private IEnumerator ReduceSpeedTemporarily()
    {
        // Меняем звук

        GameObject monk = GameObject.Find("monkWithColider");
        GameSoundManager GSM = monk.GetComponent<GameSoundManager>();

        GSM.MushroomS.TransitionTo(1.5f);

        // Сохраняем исходную скорость
        int originalSpeed = GameManager.Instance.roadSpeed;
        int speed = 6;
        diffSpeed = originalSpeed - speed;
        int speedReductionDuration = 10;

        // Уменьшаем скорость дороги
        GameManager.Instance.roadSpeed = speed;

        // Ждём 15 секунд
        yield return new WaitForSeconds(speedReductionDuration);

        // Возвращаем исходную скорость
        resetS();
    }
    public void resetS()
    {
        if (GameManager.Instance.isRunning)
        {
            GameManager.Instance.roadSpeed += diffSpeed;
            GameObject monk = GameObject.Find("monkWithColider");
            GameSoundManager GSM = monk.GetComponent<GameSoundManager>();
            GSM.Normal.TransitionTo(1.5f);
            GameManager.Instance.mushroomSpeedCoroutine = null;
        }
    }
    private IEnumerator IncreaseBananaCoef()
    {
        GameManager.Instance.CoefBanana += 1;
        int time = 20;

        yield return new WaitForSeconds(time);

        if (GameManager.Instance.CoefBanana >= 2)
            GameManager.Instance.CoefBanana -= 1;
    }
    private void ReduceBananas()
    {
        GameManager.Instance.Bananas /= 2;
    }
    private int startStep;
    private IEnumerator SwipeDayNight()
    {
        GameObject monk = GameObject.Find("monkWithColider");
        GameSoundManager GSM = monk.GetComponent<GameSoundManager>();

        GSM.MushroomDN.TransitionTo(1.5f);

        int time = 10;
        int curTime = 0;
        startStep = GameManager.Instance.nextScoreThreshold;
        GameManager.Instance.invertMovement = true;
        while (curTime <= time)
        {
            GameManager.Instance.nextScoreThreshold = 1;
            curTime += 1;
            yield return new WaitForSeconds(1);
        }

        resetDN();
    }
    public void resetDN()
    {
        GameObject monk = GameObject.Find("monkWithColider");
        GameSoundManager GSM = monk.GetComponent<GameSoundManager>();
        GSM.Normal.TransitionTo(1.5f);

        GameManager.Instance.nextScoreThreshold = startStep;
        GameManager.Instance.invertMovement = false;

        GameManager.Instance.mushroomDNCoroutine = null;
    }

}
