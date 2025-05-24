using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;
    [SerializeField] private IntValueView _priceView;
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _selectionItem; // �������
    [SerializeField] private Image _bananaIcon; // ������ ������
    [SerializeField] private TextMeshProUGUI _priceText; // ����� � �����
    [SerializeField] private GameObject _priceContainer; // ��������� ��� ���� � ������ ������
    [SerializeField] private Renderer _renderer; // ������ ��� ��������� ���������

    private Image _backgroundImage;
    private GameObject _unamedObject; // ������ �� ������ unamed

    public ShopItem Item { get; private set; }
    public bool IsLock { get; private set; }
    public int Price => Item.Price;
    public bool IsSelected { get; private set; } // ���� ���������� �����

    public event Action<ShopItemView> Click;

    public void Initialize(ShopItem item)
    {
        Item = item;
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        _contentImage.sprite = item.Icon;
        _priceView.Show(item.Price);
        _priceText.text = item.Price.ToString();


        IsLock = !item.IsUnlocked;
        _lockImage.gameObject.SetActive(IsLock);

        IsSelected = false; // ���������� �� ������
        _selectionItem.enabled = false; // ������� ������

        if (IsLock)
        {
            _priceContainer.SetActive(true); // ��������� ��� ������ � ���� �����, ���� ���� ������������
        }
        else
        {
            _priceContainer.SetActive(false); // ��������� �����, ���� ���� �������������
        }

        // ������ ������ unamed ��� ���������� ���������
        _unamedObject = GameObject.Find("Background/monkOnMenu/unamed");
        if (_unamedObject != null)
        {
            _renderer = _unamedObject.GetComponent<Renderer>(); // �������� ������ ������� unamed
        }
    }

    // ����� ����������� ��� ������� �� ����
    // ����� ����������� ��� ������� �� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ���� ��� ������, �� ������ ������� ���������
        if (IsSelected)
        {
            return;
        }

        if (IsLock)
        {
            ShopUIManager.Instance.ShowConfirmWindow(this); // ���������� ���� �������������, ���� ���� ������������
        }
        else
        {
            Click?.Invoke(this); // �������� ������� ����� ��� ����������������� �����
        }
    }

    public void ToggleSelection(List<ShopItemView> allItems)
    {
        if (IsSelected)
        {
            Unselect(); // ���� ��� ������, �������� �����
        }
        else
        {
            UnselectAllOthers(allItems); // ������� ��������� � ������ ������
            Highlight(); // ������ ��� �� ����������
            _selectionItem.enabled = true; // ���������� �������

            // ��������� �������� ������ ���� ���� ������
            if (_renderer != null && Item.SkinMaterial != null)
            {
                _renderer.material = Item.SkinMaterial; // ��������� �������� ��� ������
                GameManager.Instance.SaveSkinMaterial(Item.SkinMaterial);
                if (_renderer != null)
                {
                    // ��������� �������� �� ����� �� PlayerPrefs ��� �� ��������
                    string materialName = PlayerPrefs.GetString("SelectedSkinMaterial", string.Empty);
                    if (!string.IsNullOrEmpty(materialName))
                    {
                        Material material = Resources.Load<Material>("Materials/" + materialName);
                        if (material != null)
                        {
                            _renderer.material = material; // ��������� �������� ��� ������
                        }
                        else
                        {
                            Debug.LogError("�� ������� ����� ��������: " + materialName);
                        }
                    }
                }
            }
        }

        IsSelected = !IsSelected; // ����������� ���� ���������� �����
    }

    // ������� ��������� � ������ ������
    public void UnselectAllOthers(List<ShopItemView> allItems)
    {
        foreach (var item in allItems)
        {
            if (item != this) // ���� ��� �� ������� ����
            {
                item.Unselect(); // ������� ���������
            }
        }
    }

    // ������� ��������� � ������ ������
    private void UnselectAllOthers()
    {
        foreach (var item in ShopPanel.Instance.GetShopItems())
        {
            if (item != this) // ���� ��� �� ������� ����
            {
                item.Unselect(); // ������� ���������
            }
        }
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(false);
        Item.Unlock(); // ������������ ����

        // �������� ���� ��������� � ����� � �������, ���� ���� �������������
        _priceContainer.SetActive(false);
    }

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(true);

        // ���������� ���� ��������� � ����� � �������, ���� ���� ������������
        _priceContainer.SetActive(true);
    }

    public void Highlight()
    {
        _backgroundImage.sprite = _highlightBackground;
    }

    public void UnHighlight()
    {
        _backgroundImage.sprite = _standartBackground;
    }

    public void Unselect()
    {
        IsSelected = false;
        _selectionItem.enabled = false; // ������� �������
        _backgroundImage.sprite = _standartBackground; // ���������� ������� ���
    }
}
