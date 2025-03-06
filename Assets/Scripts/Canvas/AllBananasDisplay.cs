using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllBananasDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bananaText;

    // Update is called once per frame
    void Update()
    {
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString();
    }
}
