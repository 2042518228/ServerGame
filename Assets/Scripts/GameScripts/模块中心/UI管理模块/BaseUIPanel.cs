using System;
 
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
 

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
         FindComponent();
         ComponentAddEvent();
     }
    protected virtual void Start()
    {
        Init();
        
    }
    
    /// <summary>
    /// 事件注册
    /// </summary>
protected abstract void ComponentAddEvent();
    /// <summary>
    /// 查找组件
    /// </summary>
    protected abstract void FindComponent();
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

   public virtual void  DOTweenShow(float speed)
    {
        alphaSpeed=speed;
        transform.localScale=Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);  
    }
   public virtual void  DOTweenHide()
    {
        transform.DOScale(Vector3.zero, 0.4f);
        TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore =transform.DOLocalMove(new Vector3(1000,0,0), 0.2f);  
        tweenerCore.OnComplete(() =>
        {
            UIManagerModule.Instance.HideUIPanel(this);
        });
    }
    
}
