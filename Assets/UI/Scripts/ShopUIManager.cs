using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance;

    [Header("UI")]
    public GameObject confirmWindow; // ���� �������������
    public GameObject notEnoughWindow; // ���� ������������ �������

    [Header("Confirm Window Elements")]
    public TMP_Text confirmText; // ����� � ���� �������������
    public Button yesButton; // ������ "��"
    public Button noButton; // ������ "���"
    public Button ponButton; // ������ "���" ��� �������� ���� ��������������� �������

    private ShopItemView _currentItem; // ������� ��������� ����

    private void Awake()
    {
        Instance = this;

        // ����������� ����������� � �������
        yesButton.onClick.AddListener(OnConfirmPurchase); // ������� "��"
        noButton.onClick.AddListener(CloseConfirmWindow); // ������� "���"
        ponButton.onClick.AddListener(HideNotEnoughWindow); // ������� �� "���"
    }

    // ���������� ���� ������������� �������
    public void ShowConfirmWindow(ShopItemView itemView)
    {
        _currentItem = itemView;
        confirmText.text = $"���������� {_currentItem.Item.Name} �� {_currentItem.Price} �������?"; // ������ �����
        confirmWindow.SetActive(true); // ���������� ����
    }

    // ����� ������������� �������
    public void OnConfirmPurchase()
    {
        int currentBananas = GameManager.Instance.gameData.AllBananas;

        if (currentBananas >= _currentItem.Price) // ���������, ������� �� �������
        {
            // ���� ������� �������, �������� ����
            GameManager.Instance.gameData.AllBananas -= _currentItem.Price; // ��������� ������ �� ���� ������

            // ��������� ����������� ���������� ������� � PlayerPrefs
            PlayerPrefs.SetInt("Bananas", GameManager.Instance.gameData.AllBananas);
            PlayerPrefs.Save(); // ��������� ���������

            _currentItem.Unlock(); // ������������ ����
            _currentItem.ToggleSelection(ShopPanel.Instance.GetShopItems()); // �������� ����

            // ��������� ����������� ���������� �������
            AllBananasDisplay.Instance.Update();

            // ��������� ���������� � ���, ��� ���� ������
            SavePurchasedSkin(_currentItem);
        }
        else
        {
            ShowNotEnoughWindow(); // ���� �� ������� �������, ���������� ���� � �������
        }

        CloseConfirmWindow(); // ��������� ���� �������������
    }

    // �������� ���� �������������
    public void CloseConfirmWindow() => confirmWindow.SetActive(false);

    // ���������� ���� � �������� �������
    public void ShowNotEnoughWindow()
    {
        notEnoughWindow.SetActive(true); // ���������� ����
    }

    // ������� ���� � �������� �������
    public void HideNotEnoughWindow() => notEnoughWindow.SetActive(false);

    // ����� ��� ���������� ���������� � ������� �����
    private void SavePurchasedSkin(ShopItemView itemView)
    {
        if (itemView.Item is CharacterSkinsItem characterSkinItem)
        {
            PlayerPrefs.SetString("PurchasedSkin_" + characterSkinItem.SkinType.ToString(), "true");
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("�� ������� �������� ������ � ���� CharacterSkinsItem");
        }
    }
}
