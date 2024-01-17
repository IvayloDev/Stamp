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
    private Transform cardsTransformViewCollapsed;
    
    [SerializeField] 
    private Transform cardsTransformViewExpanded;
    
    [SerializeField]
    private CardComponent currentCardComponent;

    [SerializeField]
    private ScrollRect expandedScrollRect;
    
    [SerializeField]
    private VerticalLayoutGroup layoutGroup;
    
    [SerializeField]
    private RectTransform expandedViewScrollRectTransform;
    
    public void Start()
    {
        //Populate cards
        for (int i = 0; i < 10; i++)
        {
            InstantiateCard(i, cardsTransformViewCollapsed);
            InstantiateCard(i, cardsTransformViewExpanded);
        }

        if (expandedScrollRect != null)
            expandedScrollRect.verticalScrollbar.value = 1;
        
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
        expandedScrollRect.enabled = true;
        //cardsTransformViewCollapsed.gameObject.SetActive(true);

        float currentSpacing = layoutGroup.spacing;

// Use DoTween to animate the spacing attribute
        DOTween.To(() => currentSpacing, x => layoutGroup.spacing = x, -832, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                // This will be called when the spacing animation is complete
                expandedScrollRect.enabled = false;
                
                Vector2 currentRectTransformExpandedView = expandedViewScrollRectTransform.localPosition;
                float destinationRectTransformExpandedViewY = 500; // Replace this with your desired value

// Use DoTween to animate the localPosition.y attribute
                DOTween.To(() => currentRectTransformExpandedView.y, x => currentRectTransformExpandedView.y = x, destinationRectTransformExpandedViewY, 0.3f)
                    .OnUpdate(() => {
                        // Apply the updated localPosition during each update
                        expandedViewScrollRectTransform.localPosition = currentRectTransformExpandedView;
                    })
                    .SetEase(Ease.OutQuad);
                
            });
    }

    public void DeselectCurrentActiveCard()
    {
        expandedScrollRect.enabled = true;
        //cardsTransformViewCollapsed.gameObject.SetActive(false);
        
        float currentSpacing = layoutGroup.spacing;
        
        // Use DoTween to animate the spacing attribute
        DOTween.To(() => currentSpacing, x => layoutGroup.spacing = x, -565, 0.3f)
            .SetEase(Ease.OutQuad); // You can change the ease type as per your preference
        //expandedViewAnimator.SetBool("Collapse", false);

        
        Vector2 currentRectTransformExpandedView = expandedViewScrollRectTransform.localPosition;
        float destinationRectTransformExpandedViewY = -50; // Replace this with your desired value

// Use DoTween to animate the localPosition.y attribute
        DOTween.To(() => currentRectTransformExpandedView.y, x => currentRectTransformExpandedView.y = x, destinationRectTransformExpandedViewY, 0.3f)
            .OnUpdate(() => {
                // Apply the updated localPosition during each update
                expandedViewScrollRectTransform.localPosition = currentRectTransformExpandedView;
            })
            .SetEase(Ease.OutQuad);
        
        //collapsedViewAnimator.SetBool("Expand", true);

        //if (expandedScrollRect != null)
            //expandedScrollRect.verticalScrollbar.value = 1;
        
        //cardsTransformViewCollapsed.gameObject.SetActive(false);
        //cardsTransformViewExpanded.gameObject.SetActive(true  );
    }
    
}
