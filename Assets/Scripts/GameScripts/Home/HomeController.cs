using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomeController : MonoBehaviour
{
     public static HomeController Instance;
     private float hp;

     public float Hp
     {
          get
         {
              return hp;
         }
          set
         {
              hp = value;
              if (hp <0)
              {
                   hp = 0;
                   IsDead = true;
              }
              UpdateHpUI();
         }
     }
     private float maxhp;
     private bool isDead;
      public bool IsDead
     {
          get
          {
              return isDead;
          }
          set
          {
              isDead = value;
              if (isDead)
              {
                   //À¿Õˆ¬ﬂº≠
                   UIManagerModule.Instance.ShowUIPanel<GameOverPanel>().SetIsWinText("”Œœ∑ ß∞‹");
              }
         }
     }

     private void Awake()
     {
          if (Instance == null)
          {
                Instance=this;
          }
          else
          {
               Destroy(gameObject);
          }
     }
GamePanel gamePanel;
public void UpdateHpUI()
{
     UIManagerModule.Instance.GetUIPanel<GamePanel>().UpdateBloodValue( Hp, maxhp);
}
   public void Hit(int damage)
     {
          if (!IsDead)
          {
               Hp -= damage;
                
          }
     }
     private void OnDestroy()
     {
           Instance = null;
     }
     public void Init(float maxhp)
     {
          gamePanel = UIManagerModule.Instance.GetUIPanel<GamePanel>();
          this.maxhp = maxhp;
          Hp = maxhp;
     }
}