using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavBarNavigation : MonoBehaviour
{

    [SerializeField]
    private int defaultActiveTabIndex;
    
    [SerializeField]
    private TabElement[] tabElements;

    void Start()
    {
        foreach (var tab in tabElements)
        {
            tab.Initialize();
        }
        
        SetActiveTab(defaultActiveTabIndex);
        
    }

    public void OnTabElementClick(int index)
    {
        SetActiveTab(index);
    }

    void SetActiveTab(int index)
    {
        foreach (var tab in tabElements)
        {
            tab.DisableTab(tab);
        }
        
        if(tabElements[index] != null)
            tabElements[index].EnableTab(tabElements[index]);
    }
    
    
    
}

[Serializable]
public class TabElement
{
    public Image tabImage;
    public GameObject tabNameGO;
    public GameObject screenGO;
    public Color defaultColor;
    public Color highlightedColor;

    private Vector2 startRectPos;
    
    public void Initialize()
    {
        startRectPos = tabImage.rectTransform.anchoredPosition;
    }
    
    public void EnableTab(TabElement tab)
    {
        tab.screenGO.SetActive(true);
        tab.tabImage.DOColor(tab.highlightedColor, 0.25f);
        tab.tabNameGO.GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.25f);
        //tab.tabImage.rectTransform.DOAnchorPosY(60, .5f);
        
        // Move the tab.image.gameObject.transform up
        //float targetY = tab.tabImage.gameObject.transform.position.y + 10f; // Adjust the value as needed
        //tab.tabImage.gameObject.transform.DOMoveY(targetY, 0.25f);
    }
    
    public void DisableTab(TabElement tab)
    {
        tab.screenGO.SetActive(false);
        tab.tabImage.DOColor(tab.defaultColor, 0.25f);
        tab.tabNameGO.GetComponent<TextMeshProUGUI>().DOColor(Color.clear, 0.1f);
        //tab.tabImage.rectTransform.DOAnchorPos(startRectPos, .5f);
        
        //float originalY = tab.tabImage.gameObject.transform.position.y - 10; // Assuming the original Y position is stored somewhere
        //tab.tabImage.gameObject.transform.DOMoveY(originalY, 0.25f);
    }

    
    
}
