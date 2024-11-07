using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterShopUI : MonoBehaviour
{
    [Header ("Layout Settings")]
    [SerializeField] float itemSpacing = .5f;
    float itemHeight;

    [Header ("UI elements")]
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;
    [Space (20)]
    [SerializeField] CharacterShopDatabase characterDB;
    
    [Header("Shop Events")] 
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;
    [SerializeField] private Button scrollUpButton;

    [Space(20)] 
    [Header("Scroll View")] 
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject topScrollFade;
    [SerializeField] private GameObject bottomScrollFade;

    [Space(20)] 
    [Header("Purchase Error message")] 
    [SerializeField] private TMP_Text notEnoughCoinsText;

    private int newSelectedItemIndex = 0;
    private int previousSelectedItemIndex = 0;
    
    private AudioManager audioManager;
    
    private void Awake()
    {
	    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        AddShopEvents();
	    GenerateShopItemsUI();
	    // set selected character in the playerDataManager
	    SetSelectedCharacter();
	    // select UI item
	    SelectItemUI(GameDataManager.GetSelectedCharacterIndex());
	    AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());
    }

    private void AutoScrollShopList(int itemIndex)
    {
	    scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1f - (itemIndex /(float) (characterDB.CharactersCount-1)));
    }

    void SetSelectedCharacter()
    {
	    // get saved index
	    int index = GameDataManager.GetSelectedCharacterIndex();
	    
	    // set selected character
	    GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);
    }

	void GenerateShopItemsUI ()
	{
		for (int i = 0; i < GameDataManager.GetAllPurchasedCharacter().Count; i++)
		{
			int purchasedCharacterIndex = GameDataManager.GetPurchasedCharacter(i);
			characterDB.PurchaseCharacter(purchasedCharacterIndex);
		}
		
		//Delete itemTemplate after calculating item's Height :
		itemHeight = ShopItemsContainer.GetChild (0).GetComponent <RectTransform> ().sizeDelta.y;
		Destroy (ShopItemsContainer.GetChild (0).gameObject);
		//DetachChildren () will make sure to delete it from the hierarchy, otherwise if you 
		//write ShopItemsContainer.ChildCount you will get "1"
		ShopItemsContainer.DetachChildren ();

		//Generate Items
		for (int i = 0; i < characterDB.CharactersCount; i++) {
			//Create a Character and its corresponding UI element (uiItem)
			Character character = characterDB.GetCharacter (i);
			CharacterItemUI uiItem = Instantiate (itemPrefab, ShopItemsContainer).GetComponent <CharacterItemUI> ();

			//Move item to its position
			uiItem.SetItemPosition (Vector2.down * i * (itemHeight + itemSpacing));

			//Set Item name in Hierarchy (Not required)
			uiItem.gameObject.name = "Item" + i + "-" + character.name;

			//Add information to the UI (one item)
			uiItem.SetCharacterName (character.name);
			uiItem.SetCharacterImage (character.image);
			uiItem.SetCharacterSpeed (character.speed);
			uiItem.SetCharacterPrice (character.price);

			if (character.isPurchased) {
				//Character is Purchased
				uiItem.SetCharacterAsPurchased ();
				uiItem.OnItemSelect (i, OnItemSelected);
			} else {
				//Character is not Purchased yet
				uiItem.OnItemPurchase (i, OnItemPurchased);
			}

			//Resize Items Container
			ShopItemsContainer.GetComponent <RectTransform> ().sizeDelta = 
				Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharactersCount + itemSpacing);
		}
	}

	void AnimateNoMoreCoinsText()
	{
		notEnoughCoinsText.transform.DOComplete();
		notEnoughCoinsText.DOComplete();

		notEnoughCoinsText.transform.DOShakePosition(3f,new Vector3(5f, 0f, 0f),10,0);
		notEnoughCoinsText.DOFade(1F, 3F).From(0f).OnComplete(() =>
		{
			notEnoughCoinsText.DOFade(0f, 1f);
		});
	}
	
	void OnItemSelected(int index)
	{
		// select item in the UI
		SelectItemUI(index);
		
		// Save data
		GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);
	}

	void SelectItemUI(int itemIndex)
	{
		previousSelectedItemIndex = newSelectedItemIndex;
		newSelectedItemIndex = itemIndex;

		CharacterItemUI prevUiItem = GetItemUI(previousSelectedItemIndex);
		CharacterItemUI newUiItem = GetItemUI(newSelectedItemIndex);
		
		prevUiItem.DeSelectItem();
		newUiItem.SelectItem();
	}

	CharacterItemUI GetItemUI(int index)
	{
		return ShopItemsContainer.GetChild(index).GetComponent<CharacterItemUI>();
	}
	
    void OnItemPurchased (int index)
    {
	    Character character = characterDB.GetCharacter(index);
	    CharacterItemUI uiItem = GetItemUI(index);

	    if (GameDataManager.CanSpendCoins(character.price))
	    {
		    GameDataManager.SpendCoins(character.price);
		    
		    GameSharedUI.Instance.UpdateCoinsUIText();
		    
		    characterDB.PurchaseCharacter(index);
		    
		    uiItem.SetCharacterAsPurchased();
		    uiItem.OnItemSelect(index, OnItemSelected);
		    
		    GameDataManager.AddPurchasedCharacter(index);
	    }
	    else
	    {
		    AnimateNoMoreCoinsText();
		    uiItem.AnimateShakeItem();
	    }
    }
        
    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);
        
        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
        
        scrollRect.onValueChanged.RemoveAllListeners();
        scrollRect.onValueChanged.AddListener(OnShopListScroll);
        
        scrollUpButton.onClick.RemoveAllListeners();
        scrollUpButton.onClick.AddListener(OnScrollUpClicked);
    }

    void OnScrollUpClicked()
    {
	    scrollRect.DOVerticalNormalizedPos(1f, 5f).SetEase(Ease.OutBack);
    }
    
    void OnShopListScroll(Vector2 value)
    {
	    float scrollY = value.y;

	    if (scrollY < .1f)
	    {
		    topScrollFade.SetActive(true);
	    }
	    else
	    {
		    topScrollFade.SetActive(false);
	    }

	    if (scrollY > 0f)
	    {
		    bottomScrollFade.SetActive(true);
	    }
	    else
	    {
		    bottomScrollFade.SetActive(false);
	    }

	    if (scrollY < .7f)
	    {
		    scrollUpButton.gameObject.SetActive(true);
	    }
	    else
	    {
		    scrollUpButton.gameObject.SetActive(false);
	    }
    }
    
    void OpenShop()
    {
	    audioManager.PlaySFX(audioManager.buttons);
        shopUI.SetActive(true);
    }

    void CloseShop()
    {
	    audioManager.PlaySFX(audioManager.buttons);
        shopUI.SetActive(false);
    }
}
