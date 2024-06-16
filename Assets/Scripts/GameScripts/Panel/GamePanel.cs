using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel :BaseUIPanel
{
  
      Button quitBtn;
      TextMeshProUGUI bloodValueText;
      TextMeshProUGUI waveNumberText;
      TextMeshProUGUI playerMoneyValueText;
      Image bloodImage;
    protected override void Awake()
    {
        base.Awake();
        InitializeUIElements();
    }

    protected override void ComponentAddEvent()
    {
        throw new NotImplementedException();
    }

    protected override void FindComponent()
    {
        throw new NotImplementedException();
    }

    public Transform fortsTrasform;
    private void InitializeUIElements()
    {
         fortsTrasform = transform.Find("Forts");
         
        quitBtn=transform.Find("QuitBt").GetComponent<Button>();
        quitBtn.onClick.AddListener(()=>OnQuitButtonClick());
        bloodValueText = transform.Find("BloodValueText").GetComponent<TextMeshProUGUI>();
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        playerMoneyValueText = transform.Find("PlayerMoneyValueText").GetComponent<TextMeshProUGUI>();
        bloodImage = transform.Find("BloodImage").GetComponent<Image>();
        
        fortsTrasform.gameObject.SetActive(false);
    }
   
    private void OnQuitButtonClick()
    {
        UIManagerModule.Instance.HideUIPanel(this);
        SceneManager.LoadScene(Scene.BeginScene.ToString());
    }
  
    protected override void Init()
    {
         SetUIPanelData();
    }

     void SetUIPanelData()
    {
       PlayerData playerData = GameDataManager.Instance.GetData<PlayerData>();
       playerMoneyValueText.text = "￥"+playerData.money;
     
    }

  
    public void  UpdateBloodValue(float nowblood, float maxblood)
    {
        
        bloodValueText.text = nowblood.ToString("F0")+"/"+maxblood.ToString("F0");
        bloodImage.fillAmount =(nowblood / maxblood);
    }
    public void UpdateWaveNumber(int waveNumber,int maxWaveNumber)
    {
        waveNumberText.text = $"{waveNumber}/{maxWaveNumber}";
    }
    public void UpdatePlayerMoney(int money)
    {
        playerMoneyValueText.text = "￥"+money;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.Locked;
    }


}
 
