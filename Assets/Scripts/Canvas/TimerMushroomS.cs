using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerMushroomS : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.timerMushroomS + 1 > 0)
            timer.text = (GameManager.Instance.timerMushroomS + 1).ToString();
    }
}
