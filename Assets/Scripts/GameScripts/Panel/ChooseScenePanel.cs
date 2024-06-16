using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChooseScenePanel :BaseUIPanel
    {
        public Button sureBtn;
    public Button backBtn;
    public Button leftBtn;
    public Button rightBtn;
     public TextMeshProUGUI sceneNameText;
     public TextMeshProUGUI sceneInfoText;
    public Image sceneImage;
    private int sceneId=0;
    private List<SceneInfo> SceneInfoList;
public int SceneId
    {
        get { return sceneId; }
        set
        {
            sceneId = value;
            if (sceneId<0)
            {
                sceneId = SceneInfoList.Count-1;
            }
            if (sceneId>SceneInfoList.Count-1)
            {
                sceneId =0;
            }
            ChooseScene();
        }
    }

    private SceneInfo sceneInfo;
    private void ChooseScene()
    {
         sceneInfo = SceneInfoList[sceneId]; 
        sceneNameText.text = sceneInfo.name;
        sceneInfoText.text = sceneInfo.info;
        sceneImage.sprite = Resources.Load<Sprite>(sceneInfo.imgRes);
    }

    protected override void Awake()
    {
        base.Awake();
         InitializeUIElements();
        InitializeButtons();
    }

    protected override void ComponentAddEvent()
    {
        throw new NotImplementedException();
    }

    protected override void FindComponent()
    {
        throw new NotImplementedException();
    }

    private void InitializeUIElements()
    {
        Transform sceneInfotTransform= transform.Find("SceneInfo");
        sceneNameText = sceneInfotTransform.Find("SceneNameText").GetComponent<TextMeshProUGUI>();
        sceneInfoText =  sceneInfotTransform.Find("SceneInfoText").GetComponent<TextMeshProUGUI>();
        sceneImage = sceneInfotTransform.Find("SceneImage").GetComponent<Image>();
    }

    protected override void Init()
    {
        InitializeGameData();
        ChooseScene();
    }
    
    private void InitializeButtons()
    {
        sureBtn = FindAndInitializeButton("SureBt", OnSureButtonClick);
        leftBtn = FindAndInitializeButton("LeftBt", OnLeftButtonClick);
        rightBtn = FindAndInitializeButton("RightBt", OnRightButtonClick);
        backBtn = FindAndInitializeButton("BackBt", OnBackButtonClick);
    }

    private Button FindAndInitializeButton(string buttonName, Action onClickAction)
    {
        var button = transform.Find(buttonName).GetComponent<Button>();
        button.onClick.AddListener(() => onClickAction());
        return button;
    }

     PlayerData playerData;
    private void InitializeGameData()
    {
        playerData = GameDataManager.Instance.GetData<PlayerData>();
        SceneInfoList = GameDataManager.Instance.GetData<List<SceneInfo>>("GameSceneInfoList");
    }

    private void OnSureButtonClick()
    {
     AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(sceneInfo.sceneName);
     asyncOperation.completed += (operation) =>
     {
         UIManagerModule.Instance.HideUIPanel<ChooseScenePanel>();
         UIManagerModule.Instance.ShowUIPanel<GamePanel>();
     };
    }

    private void OnLeftButtonClick()
    {
        SceneId--;
    }

    private void OnRightButtonClick()
    {
        
        SceneId++;
    }

    private void OnBackButtonClick()
    {
    }
 
    }