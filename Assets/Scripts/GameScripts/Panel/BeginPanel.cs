using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeginPanel : BaseUIPanel
{
    public Button signBtn;
    public Button quitBtn;
    public Button settingBtn;
    
    protected override void Start()
    {
        base.Start();
    }

     

    protected override void Awake()
    { 
        signBtn = transform.Find("SignBtn").GetComponent<Button>();
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        quitBtn= transform.Find("QuitBtn").GetComponent<Button>(); 
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        signBtn.onClick.AddListener(() => { });
        settingBtn.onClick.AddListener(() => { UIManagerModule.Instance.ShowUIPanel<SetPanel>(); });

        quitBtn.onClick.AddListener(() => { Application.Quit(); });
    }
}
