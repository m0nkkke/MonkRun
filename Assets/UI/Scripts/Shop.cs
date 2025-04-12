using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;  // Содержит все скины
    [SerializeField] private ShopPanel _shopPanel;  // Панель скинов, которая будет их отображать

    private IDataProvider _dataProvider;
    private Wallet _wallet;
    private SkinUnLocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;

    // Инициализация
    public void Initialize(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, SkinUnLocker skinUnlocker)
    {
        _wallet = wallet;
        _openSkinsChecker = openSkinsChecker;
        _skinUnlocker = skinUnlocker;
        _dataProvider = dataProvider;

        ShowAllSkins();  // Показываем все скины
    }

    private void Start()
    {
        Initialize(_dataProvider, _wallet, _openSkinsChecker, _skinUnlocker);
    }

    // Показываем все скины из ShopContent
    private void ShowAllSkins()
    {
        Debug.Log("Метод ShowAllSkins вызван");  // Логируем вызов метода

        if (_contentItems == null)
        {
            Debug.LogError("ShopContent не был найден!");  // Логируем если _contentItems не привязан
            return;
        }

        if (_contentItems.CharacterSkinsItems == null)
        {
            Debug.LogError("ShopContent не содержит скинов.");  // Логируем если скины не привязаны
        }
        else
        {
            Debug.Log("Передаем скины в ShopPanel...");
            _shopPanel.Show(_contentItems.CharacterSkinsItems);  // Передаем скины в ShopPanel
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
//        Debug.Log("Автозагрузка товаров магазина...");
//        _shopPanel.Show(_contentItems.CharacterSkins);  // Показываем товары
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
