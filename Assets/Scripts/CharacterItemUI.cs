using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CharacterItemUI : MonoBehaviour
{
    [SerializeField] private Color itemNotSelectedColor;
    [SerializeField] private Color itemSelectedColor;
    
    [Space (20f)]
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private Image characterSpeedFill;
    [SerializeField] private TMP_Text characterPriceText;
    [SerializeField] private Button characterPurchaseButton;

    [Space(20f)] 
    [SerializeField] private Button itemButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private Outline itemOutline;

    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void SetCharacterName(string name)
    {
        characterNameText.text = name;
    }

    public void SetCharacterSpeed(float speed)
    {
        characterSpeedFill.fillAmount = speed / 100;
    }

    public void SetCharacterPrice(int price)
    {
        characterPriceText.text = price.ToString();
    }

    public void SetCharacterAsPurchased()
    {
        characterPurchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;

        itemImage.color = itemNotSelectedColor;
    }

    public void OnItemPurchase(int itemIdex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        characterPurchaseButton.onClick.RemoveAllListeners();
        characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIdex));
    }

    public void SelectItem()
    {
        itemOutline.enabled = true;
        itemImage.color = itemSelectedColor;
        itemButton.interactable = false;
    }
    
    public void DeSelectItem()
    {
        itemOutline.enabled = false;
        itemImage.color = itemNotSelectedColor;
        itemButton.interactable = true;
    }
}
