using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllBananasDisplay : MonoBehaviour
{
    [SerializeField]
    private Text bananaText;

    // Update is called once per frame
    void Update()
    {
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString();
    }
}
