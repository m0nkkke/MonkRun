using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReviveCostDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text reviveCostText;
    private void Start()
    {
        reviveCostText.text = $"{GameManager.Instance.CostRevive.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        reviveCostText.text = $"{GameManager.Instance.CostRevive.ToString()}";
    }
}
