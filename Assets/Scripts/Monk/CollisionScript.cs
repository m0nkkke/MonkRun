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
            //collision.collider.enabled = false;
            Collider[] colliders = collision.gameObject.GetComponents<Collider>();

            // ������� ��� ����������
            foreach (Collider col in colliders)
            {
                Destroy(col);
            }
            //GameManager.Instance.roadSpeed = 0;
            animator.Play("Hit On Legs", -1, 0f);

            GameManager.Instance.OnMonkCollision(ColliderTypes.Rock);
            //GameManager.Instance.ResetGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        //print(tag);
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
        // ������ ����

        GameObject monk = GameObject.Find("monkWithColider");
        GameSoundManager GSM = monk.GetComponent<GameSoundManager>();

        GSM.MushroomS.TransitionTo(1.5f);

        // ��������� �������� ��������
        int originalSpeed = GameManager.Instance.roadSpeed;
        int speed = 6;
        diffSpeed = originalSpeed - speed;
        int speedReductionDuration = 10;
        GameManager.Instance.timerMushroomS = speedReductionDuration + 1;
        GameManager.Instance.textTimerMushroomS.SetActive(true);

        // ��������� �������� ������
        GameManager.Instance.roadSpeed = speed;
        while (GameManager.Instance.timerMushroomS > 0)
        {
            GameManager.Instance.timerMushroomS--;
            yield return new WaitForSeconds(1);
        }
        // ��� 15 ������

        // ���������� �������� ��������
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
            GameManager.Instance.textTimerMushroomS.SetActive(false);
            GameManager.Instance.timerMushroomS = 0;
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
        //int curTime = 0;
        GameManager.Instance.timerMushroomDN = time + 1;
        GameManager.Instance.textTimerMushroomDN.SetActive(true);
        startStep = GameManager.Instance.nextScoreThreshold;
        GameManager.Instance.invertMovement = true;
        while (GameManager.Instance.timerMushroomDN > 0)
        {
            GameManager.Instance.nextScoreThreshold = 1;
            GameManager.Instance.timerMushroomDN--;
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
        GameManager.Instance.textTimerMushroomDN.SetActive(false);
        GameManager.Instance.timerMushroomDN = 0;
    }

}
