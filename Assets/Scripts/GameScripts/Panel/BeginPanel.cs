using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeginPanel : BaseUIPanel
{
      Button signBtn;
      Button quitBtn;
      Button settingBtn;
    
   
    protected override void ComponentAddEvent()
    {
       signBtn.onClick.AddListener(() =>
       {
           UIManagerModule.Instance.ShowUIPanel<SginPanel>();
           DOTweenHide();
       });
       settingBtn.onClick.AddListener(() => { UIManagerModule.Instance.ShowUIPanel<SetPanel>(); });

       quitBtn.onClick.AddListener(() => { Application.Quit(); });
    }

    protected override void FindComponent()
    {
        signBtn = transform.Find("SignBtn").GetComponent<Button>();
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        quitBtn= transform.Find("QuitBtn").GetComponent<Button>(); 
    }
 
    protected override void Init()
    {
       
    }
}
