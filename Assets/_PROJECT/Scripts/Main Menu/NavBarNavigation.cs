using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBarNavigation : MonoBehaviour
{
    [SerializeField]
    private TabElement[] tabElements;

    void Start()
    {
        SetActiveTab(1);
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

    public void DisableTab(TabElement tab)
    {
        tab.screenGO.SetActive(false);
        tab.tabImage.color = tab.defaultColor;
        tab.tabNameGO.SetActive(false);
    }

    public void EnableTab(TabElement tab)
    {
        tab.screenGO.SetActive(true);
        tab.tabImage.color = tab.highlightedColor;
        tab.tabNameGO.SetActive(true);
    }
    
}
