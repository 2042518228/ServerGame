using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UIManagerModule : Singleton<UIManagerModule>
{
   public Dictionary<string, BaseUIPanel> uiPanels = new Dictionary<string, BaseUIPanel>();
public Transform canvsTsf;

public UIManagerModule()
{
   canvsTsf = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas")).transform;
   if (canvsTsf != null)
   {
      GameObject.DontDestroyOnLoad(canvsTsf);
   }
   else
   {
      Debug.LogError("UI/Canvas is null");
   }
}

/// <summary>
   /// 显示面板
   /// </summary>
   /// <param name="name"></param>
   /// <returns></returns>
   public T ShowUIPanel<T>()where T:BaseUIPanel
   {
      string name = typeof(T).Name;
      if (uiPanels.ContainsKey(name))
      {
         return uiPanels[name] as T ;
      }
     // 创建面板
      GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + name));
      if (go != null)
      {
         go.transform.SetParent(canvsTsf,false);
         T t = go.GetComponent<T>();
         if (t != null)
         {
            uiPanels.Add(name, t);
            t.ShowMe();
            return t;
         }
         else
         {
            Debug.LogError("UI/" + name + " is null");
         }  
      }else
      {
         Debug.LogError("UI/" + name + " is null");
      }
      return null;
   }
/// <summary>
/// 隐藏面板
/// </summary>
/// <param name="name"></param>
   public void HideUIPanel<T>(bool isShowHide = false)where T:BaseUIPanel
   {
      string name = typeof(T).Name;
      if (uiPanels.ContainsKey(name))
      {
         if (isShowHide)
         {
            GameObject.Destroy(uiPanels[name].gameObject);
         }
         else
         {
            uiPanels[name].HideMe(() =>
            {
               
               GameObject.Destroy(uiPanels[name].gameObject);
               uiPanels.Remove(name);
            });
         }
         
      }
   }
   public void HideUIPanel(BaseUIPanel baseUIPanel,bool isShowHide = false) 
   {
      string name = baseUIPanel.GetType().Name;
      if (uiPanels.ContainsKey(name))
      {
         if (isShowHide)
         {
            GameObject.Destroy(uiPanels[name].gameObject);
         }
         else
         {
            uiPanels[name].HideMe(() =>
            {
               
               GameObject.Destroy(uiPanels[name].gameObject);
               uiPanels.Remove(name);
            });
         }
         
      }
   }
public T GetUIPanel<T>()where T:BaseUIPanel
{
   string name = typeof(T).Name;
   if (uiPanels.ContainsKey(name))
   {
      return uiPanels[name] as T;
   }

   return null;
}
}
