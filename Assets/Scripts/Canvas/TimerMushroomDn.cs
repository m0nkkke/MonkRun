using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerMushroomDn : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer;
    //[SerializeField]
    //private Image icon;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.timerMushroomDN > 0)
        {
            timer.text = GameManager.Instance.timerMushroomDN.ToString();
            //icon.gameObject.SetActive(true);
        }
        //else icon.gameObject.SetActive(false);

    }
}
