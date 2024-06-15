using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseUIPanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected float alphaSpeed =0.5f;
     protected virtual void Awake()
     {
         canvasGroup = GetComponent<CanvasGroup>();
         if (canvasGroup==null)
         {
           canvasGroup= transform.AddComponent<CanvasGroup>();
            
         }
     }
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected abstract void Init();
protected bool isShow=false;
protected UnityAction onHideAction;
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }
    public  virtual void HideMe(UnityAction action=null)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        onHideAction=action;
    }

    protected virtual void Update()
    {
        if (isShow)
        {
             canvasGroup.alpha += alphaSpeed * Time.deltaTime;
             if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }   
        }
        else
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;   
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha=0;
                onHideAction?.Invoke();
            }
        }
    }
}
