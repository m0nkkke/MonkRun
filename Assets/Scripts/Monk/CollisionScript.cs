using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : SoundsScript
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
            GameManager.Instance.roadSpeed = 0;
            animator.Play("Hit On Legs", -1, 0f);
            GameManager.Instance.ResetGame();
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
                StartCoroutine(SwipeDayNight());
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
                StartCoroutine(ReduceSpeedTemporarily());
                break;
        }
        if (flagDestroy) Destroy(other.gameObject);

        //if (other.gameObject.tag == "Banana")
        //{
        //    GameManager.Instance.Bananas += 1;
        //    PlaySound(sounds[0], volume: 0.2f);
        //    Destroy(other.gameObject);
        //}
        //else if (other.gameObject.tag == "MushroomDN")
        //{
            
        //}
    }
    private void Update()
    {
        //if (GameManager.Instance.Score > 10)
        //{
        //    GameManager.Instance.roadSpeed = 10;
        //}
        //else if (GameManager.Instance.Score < 10 && GameManager.Instance.roadSpeed == 10)
        //{
        //    GameManager.Instance.roadSpeed = 8;
        //}
    }
    private IEnumerator ReduceSpeedTemporarily()
    {
        // Сохраняем исходную скорость
        int originalSpeed = GameManager.Instance.roadSpeed;
        int speed = 6;
        int diff = originalSpeed - speed;
        int speedReductionDuration = 15;

        // Уменьшаем скорость дороги
        GameManager.Instance.roadSpeed = speed;

        // Ждём 15 секунд
        yield return new WaitForSeconds(speedReductionDuration);

        // Возвращаем исходную скорость
        if (GameManager.Instance.isRunning)
            GameManager.Instance.roadSpeed += diff;
    }
    private IEnumerator IncreaseBananaCoef()
    {
        GameManager.Instance.CoefBanana += 1;
        int time = 20;

        yield return new WaitForSeconds(time);

        GameManager.Instance.CoefBanana -= 1;
    }
    private void ReduceBananas()
    {
        GameManager.Instance.Bananas /= 2;
    }
    private IEnumerator SwipeDayNight()
    {
        int time = 20;

        // Тут надо вызвать быструю смену дня и ночи
        // если это какой то кэф то просто изменить
        // если функция то можно в цикл бахнуть
        print("swipe");

        yield return new WaitForSeconds(time);
    }

}
