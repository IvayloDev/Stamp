using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI placeName;
    
    [SerializeField]
    private TextMeshProUGUI placeDesc;
    
    [SerializeField]
    private Image placeImage;

    private WalletController walletController;
    
    public WalletController WalletController
    {
        get { return walletController; }
        set { walletController = value; }
    }
    
    public void OnCardButtonClick()
    {
        walletController?.SelectCurrentActiveCard(this);
    }

    public void UpdateCardInfo(CardComponent newCard)
    {
        placeName.text = newCard.placeName.text;
        placeDesc.text = newCard.placeDesc.text;
        placeImage.sprite = newCard.placeImage.sprite;
        placeImage.color = newCard.placeImage.color;
    }
    
}
