using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {
        AddShopEvents();
	    GenerateShopItemsUI();
    }

	void GenerateShopItemsUI ()
	{	
		//Delete itemTemplate after calculating item's Height :
		itemHeight = ShopItemsContainer.GetChild (0).GetComponent <RectTransform> ().sizeDelta.y;
		Destroy (ShopItemsContainer.GetChild (0).gameObject);
		//DetachChildren () will make sure to delete it from the hierarchy, otherwise if you 
		//write ShopItemsContainer.ChildCount you w'll get "1"
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

	void OnItemSelected(int index)
	{
		Debug.Log ("select" + index);
	}
    
    void OnItemPurchased (int index)
    {
        Debug.Log ("purchase" + index);
    }
        
    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);
        
        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
