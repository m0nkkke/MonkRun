using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;
    [SerializeField] private ShopPanel _shopPanel;

    private void Start()
    {
        ShowDefaultCategory();
    }

    private void ShowDefaultCategory()
    {
        Debug.Log("������������ ������� ��������...");
        _shopPanel.Show(_contentItems.CharacterSkins.Cast<ShopItem>());
    }
}
