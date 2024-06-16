using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimePanel :BaseUIPanel
{
    public TextMeshProUGUI tipText;
    protected override void Awake()
    {
        base.Awake();
        alphaSpeed=1f;
        tipText=transform.Find("TipText").GetComponent<TextMeshProUGUI>();
    }

    protected override void ComponentAddEvent()
    {
        throw new System.NotImplementedException();
    }

    protected override void FindComponent()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        
    }

 
    public void ShowTip(string tip)
    {
        tipText.text = tip;
        UIManagerModule.Instance.HideUIPanel(this);
    }
}
