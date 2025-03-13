using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllBananasWithCurBananas : MonoBehaviour
{
    [SerializeField]
    private TMP_Text allBananasText;
    private void Start()
    {
        allBananasText.text = $"Bananas: {GameManager.Instance.gameData.AllBananas.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        allBananasText.text = $"Bananas {GameManager.Instance.gameData.AllBananas + GameManager.Instance.Bananas}";
    }
}
