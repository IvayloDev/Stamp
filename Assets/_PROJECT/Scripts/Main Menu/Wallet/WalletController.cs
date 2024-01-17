using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WalletController : MonoBehaviour
{
    [SerializeField] 
    private GameObject cardPrefab;
    
    [SerializeField] 
    private Transform cardsHolderTransform;
    
    [SerializeField]
    private ScrollRect expandedScrollRect;
    
    [SerializeField]
    private VerticalLayoutGroup verticalLayoutGroup;
    
    [SerializeField]
    private RectTransform ScrollRectTransform;
    
    [SerializeField]
    private ContentSizeFitter contentSizeFitterComponent;

    [SerializeField]
    private GameObject DeselectActiveCardButton;
    
    public void Start()
    {
        DeselectActiveCardButton.SetActive(false);

        //Populate cards
        for (int i = 0; i < 10; i++)
        {
            InstantiateCard(i, cardsHolderTransform);
        }

        if (expandedScrollRect != null)
        {
            
        }
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

    private Transform currentSelectedCardTransform;
    float targetValue = 1f;
    float duration = 0.1f;
    private int childOriginalIndex = 0;
    private float originalSpacing;
    private int originalBottomPadding;
    
    public void SelectCurrentActiveCard(CardComponent cardComponent)
    {
        currentSelectedCardTransform = cardComponent.transform;
        childOriginalIndex = currentSelectedCardTransform.GetSiblingIndex();

        originalSpacing = verticalLayoutGroup.spacing;
        originalBottomPadding = verticalLayoutGroup.padding.bottom;
        
        //Set children as first
        currentSelectedCardTransform.SetAsFirstSibling();
        
        DOTween.To(() => expandedScrollRect.verticalScrollbar.value, 
                x => expandedScrollRect.verticalScrollbar.value = x, 
                targetValue, 
                duration)
            .OnComplete(() =>
            {
                // This lambda function is called when the animation is complete
                // You can perform additional actions here if needed
                expandedScrollRect.verticalScrollbar.value = 1;

                currentSelectedCardTransform.SetParent(expandedScrollRect.transform);
                currentSelectedCardTransform.SetAsFirstSibling();

                //Content Size fitter
                contentSizeFitterComponent.enabled = false;
        
                //Vertical layout group
                verticalLayoutGroup.enabled = true;

                AnimateLayoutSpacing(-717, true);

                ScaleDownChildren(cardsHolderTransform);
            });
    }

    float initialScalePercentage = 1.0f; // Initial scale percentage
    private float scaleReduction = 0.02f;
    
    void ScaleDownChildren(Transform parent)
    {
        int childCount = parent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            float scalePercentage = initialScalePercentage - (childCount - 1 - i) * scaleReduction;

            // Use DoTween to scale the child down along the X-axis only
            child.DOScaleX(child.localScale.x * scalePercentage, 0.1f)
                .SetEase(Ease.OutQuad);
        }
    }
    
    void RestoreAllScales(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);

            // Reset the scale to (1, 1, 1)
            child.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void AnimateLayoutSpacing(float endValue, bool activateDeselectButton, Action onCompleteCallback = null)
    {
        float currentSpacing = verticalLayoutGroup.spacing;
        
        DOTween.To(() => currentSpacing, x => verticalLayoutGroup.spacing = x, endValue, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DeselectActiveCardButton.SetActive(activateDeselectButton);
                
                onCompleteCallback?.Invoke();
            });
    }
    
    // void AnimateLayoutBottomPadding(float endValue)
    // {
    //     RectOffset currentPadding = verticalLayoutGroup.padding;
    //     
    //     DOTween.To(() => currentPadding.bottom, x => currentPadding.bottom = x, endValue, 0.2f)
    //         .SetEase(Ease.OutQuad)
    //         .OnUpdate(() =>
    //         {
    //             // Apply the updated padding during the animation
    //             verticalLayoutGroup.padding = currentPadding;
    //         })
    //         .OnComplete(() =>
    //         {
    //             // Animation complete logic, if needed
    //         });
    // }

    public void DeselectCurrentActiveCard()
    {
        if (currentSelectedCardTransform == null)
            return;
        
        RestoreAllScales(cardsHolderTransform);
            
        
        AnimateLayoutSpacing(originalSpacing, false, () =>
        {
            currentSelectedCardTransform.SetParent(cardsHolderTransform);
            Debug.LogError(currentSelectedCardTransform.parent, currentSelectedCardTransform.parent.gameObject);
            currentSelectedCardTransform.SetSiblingIndex(childOriginalIndex);
            
            contentSizeFitterComponent.enabled = true;

            verticalLayoutGroup.enabled = true;
        });
        
        
    }
    
}
