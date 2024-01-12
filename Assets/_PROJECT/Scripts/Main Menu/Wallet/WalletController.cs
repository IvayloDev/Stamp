using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    [SerializeField] 
    private GameObject cardPrefab;
    
    [SerializeField] 
    private Transform cardsTransformViewCollapsed;
    
    [SerializeField] 
    private Transform cardsTransformViewExpanded;
    
    [SerializeField] 
    private GameObject currentCardHolder;

    [SerializeField]
    private CardComponent currentCardComponent;
    
    public void Start()
    {
        currentCardHolder.SetActive(false);
        
        //Populate cards
        for (int i = 0; i < 10; i++)
        {
            InstantiateCard(i, cardsTransformViewCollapsed);
            InstantiateCard(i, cardsTransformViewExpanded);
        }
        
        cardsTransformViewCollapsed.gameObject.SetActive(false);
        cardsTransformViewExpanded.gameObject.SetActive(true);
    }

    void InstantiateCard(int id, Transform transform)
    {
        var _cardPrefab = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, transform);
        _cardPrefab.transform.SetParent(transform, false);

        _cardPrefab.TryGetComponent<CardComponent>(out var cardComponent);

        if (cardComponent != null)
        {
            cardComponent.WalletController = this;
            cardComponent.placeName.text = cardComponent.placeName.text + id.ToString();
        }
    }


    public void SelectCurrentActiveCard(CardComponent cardComponent)
    {
        currentCardHolder.SetActive(true);
        currentCardComponent.UpdateCardInfo(cardComponent);
        
        cardsTransformViewCollapsed.gameObject.SetActive(true);
        cardsTransformViewExpanded.gameObject.SetActive(false);

    }

    public void DeselectCurrentActiveCard()
    {
        currentCardHolder.SetActive(false);
        
        cardsTransformViewCollapsed.gameObject.SetActive(false);
        cardsTransformViewExpanded.gameObject.SetActive(true);
    }
    
}
