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

    public void EnableTab(TabElement tab)
    {
        tab.screenGO.SetActive(true);
        tab.tabImage.DOColor(tab.highlightedColor, 0.25f);
        tab.tabNameGO.GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.25f);
    }
    
    public void DisableTab(TabElement tab)
    {
        tab.screenGO.SetActive(false);
        tab.tabImage.DOColor(tab.defaultColor, 0.25f);
        tab.tabNameGO.GetComponent<TextMeshProUGUI>().DOColor(Color.clear, 0.1f);

    }

    
    
}
