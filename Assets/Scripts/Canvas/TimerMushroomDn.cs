using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerMushroomDn : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.timerMushroomDN > 0)
            timer.text = GameManager.Instance.timerMushroomDN.ToString();
    }
}
