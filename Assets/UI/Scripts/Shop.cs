using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;  // �������� ��� �����
    [SerializeField] private ShopPanel _shopPanel;  // ������ ������, ������� ����� �� ����������

    private IDataProvider _dataProvider;
    private Wallet _wallet;
    private SkinUnLocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;

    // �������������
    public void Initialize(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, SkinUnLocker skinUnlocker)
    {
        _wallet = wallet;
        _openSkinsChecker = openSkinsChecker;
        _skinUnlocker = skinUnlocker;
        _dataProvider = dataProvider;

        ShowAllSkins();  // ���������� ��� �����
    }

    private void Start()
    {
        Initialize(_dataProvider, _wallet, _openSkinsChecker, _skinUnlocker);
    }

    // ���������� ��� ����� �� ShopContent
    private void ShowAllSkins()
    {
        Debug.Log("����� ShowAllSkins ������");  // �������� ����� ������

        if (_contentItems == null)
        {
            Debug.LogError("ShopContent �� ��� ������!");  // �������� ���� _contentItems �� ��������
            return;
        }

        if (_contentItems.CharacterSkinsItems == null)
        {
            Debug.LogError("ShopContent �� �������� ������.");  // �������� ���� ����� �� ���������
        }
        else
        {
            Debug.Log("�������� ����� � ShopPanel...");
            _shopPanel.Show(_contentItems.CharacterSkinsItems);  // �������� ����� � ShopPanel
        }
    }
}






//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class Shop : MonoBehaviour
//{
//    [SerializeField] private ShopContent _contentItems;
//    [SerializeField] private ShopCategoryButton _characterSkinsButton;
//    [SerializeField] private Button _selectionButton;
//    [SerializeField] private Image _selectedText;
//    [SerializeField] private ShopPanel _shopPanel;

//    private IDataProvider _dataProvider;
//    private ShopItemView _previewedItem;
//    private Wallet _wallet;
//    private SkinSelector _skinSelector;
//    private SkinUnLocker _skinUnlocker;
//    private OpenSkinsChecker _openSkinsChecker;
//    private SelectedSkinChecker _selectedSkinChecker;

//    private void OnEnable()
//    {
//        _characterSkinsButton.Click += OnCharacterSkinsButtonClick;
//        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
//    }

//    private void OnDisable()
//    {
//        _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
//        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
//    }

//    public void Initialize(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnLocker skinUnlocker)
//    {
//        _wallet = wallet;
//        _openSkinsChecker = openSkinsChecker;
//        _selectedSkinChecker = selectedSkinChecker;
//        _skinSelector = skinSelector;
//        _skinUnlocker = skinUnlocker;
//        _dataProvider = dataProvider;
//        _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);

//        OnCharacterSkinsButtonClick();
//    }

//    private void Start()
//    {
//        ShowDefaultCategory();
//    }

//    private void ShowDefaultCategory()
//    {
//        Debug.Log("������������ ������� ��������...");
//        _shopPanel.Show(_contentItems.CharacterSkins);  // ���������� ������
//    }

//    private void OnCharacterSkinsButtonClick()
//    {
//        _characterSkinsButton.Select();
//        _shopPanel.Show(_contentItems.CharacterSkins);
//    }

//    private void OnSelectionButtonClick()
//    {
//        SelectSkin();
//        _dataProvider.Save();
//    }

//    private void SelectSkin()
//    {
//        _skinSelector.Visit(_previewedItem.Item);
//        _shopPanel.Select(_previewedItem);
//        ShowSelectedText();
//    }

//    private void ShowSelectedText()
//    {
//        _selectedText.gameObject.SetActive(true);
//        HideSelectionButton();
//    }

//    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
//}
