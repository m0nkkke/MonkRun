using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BananasCoefDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bananaCoefText;

    // Update is called once per frame
    void Update()
    {
        bananaCoefText.text = "x" + GameManager.Instance.CoefBanana.ToString();
    }
}
