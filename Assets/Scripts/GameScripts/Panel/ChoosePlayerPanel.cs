using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 

public class ChoosePlayerPanel : BaseUIPanel
{
    public Button sureBtn;
    public Button backBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button shopButton;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI roleMoneyValue;
    public Transform rolePosTransform;
    public PlayerData playerData;

    private int roleId;
    private List<RoleInfo> roleList;
    private GameObject roleObj;

    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
        InitializeUIElements();
    }
    
    protected override void Init()
    {
        InitializePlayerData();
        ChoosePlayer();   
    }
    
    private void InitializeButtons()
    {
        sureBtn = FindAndInitializeButton("SureBt", OnSureButtonClick);
        leftBtn = FindAndInitializeButton("LeftBt", OnLeftButtonClick);
        rightBtn = FindAndInitializeButton("RightBt", OnRightButtonClick);
        backBtn = FindAndInitializeButton("BackBt", OnBackButtonClick);
        shopButton = FindAndInitializeButton("ShopBt", OnShopButtonClick);
    }

    private Button FindAndInitializeButton(string buttonName, Action onClickAction)
    {
        var button = transform.Find(buttonName).GetComponent<Button>();
        button.onClick.AddListener(() => onClickAction());
        return button;
    }

    private void InitializeUIElements()
    {
        characterName = transform.Find("CharacterNameTip").GetComponentInChildren<TextMeshProUGUI>();
        moneyValue = transform.Find("MoneyValueText").GetComponentInChildren<TextMeshProUGUI>();
        roleMoneyValue = shopButton.GetComponentInChildren<TextMeshProUGUI>();
        rolePosTransform = GameObject.Find("RolePos").transform;
    }

    private void InitializePlayerData()
    {
        playerData = GameDataManager.Instance.GetData<PlayerData>();
        roleList = GameDataManager.Instance.GetData<List<RoleInfo>>("RoleInfoList");
        UpdatePlayerMoneyText(playerData.money);
    }

    private void OnSureButtonClick()
    {
        if (playerData.nowRoleId != 0)
        {
            playerData.nowRoleId = RoleId + 1;
            GameDataManager.Instance.SaveData(playerData);
            UIManagerModule.Instance.HideUIPanel(this);
            UIManagerModule.Instance.ShowUIPanel<ChooseScenePanel>();
        }
    }

    private void OnLeftButtonClick()
    {
        RoleId--;
    }

    private void OnRightButtonClick()
    {
        RoleId++;
    }

    private void OnBackButtonClick()
    {
      
    }

    private void OnShopButtonClick()
    {
        RoleInfo selectedRole = roleList[roleId];
        if (playerData.money >= selectedRole.lockMoney)
        {
            UnlockRole(selectedRole);
             UIManagerModule.Instance.ShowUIPanel<TipPanel>().ShowTip("购买成功");
        }
        else
        {
            UIManagerModule.Instance.ShowUIPanel<TipPanel>().ShowTip("金钱不足");
        }
    }

    private void UnlockRole(RoleInfo roleInfo)
    {
        playerData.money -= roleInfo.lockMoney;
        playerData.roleIdList.Add(roleInfo.id);
        UpdateUIWithSelectedRole(roleInfo);
        UpdatePlayerMoneyText(playerData.money);
    }

    public int RoleId
    {
        get { return roleId; }
        set
        {
            roleId = (value < 0) ? roleList.Count - 1 : value % roleList.Count;
            ChoosePlayer();
        }
    }

    public void ChoosePlayer()
    {
        if (roleList != null && roleList.Count > 0)
        {
            RoleInfo selectedRole = roleList[roleId];
            ClearExistingRoleObject();
            UpdateUIWithSelectedRole(selectedRole);
            SpawnNewRoleObject(selectedRole);
        }
    }

    private void ClearExistingRoleObject()
    {
        if (roleObj != null)
        {
            Destroy(roleObj);
            roleObj = null;
        }
    }

    private void UpdateUIWithSelectedRole(RoleInfo roleInfo)
    {
        bool isRoleUnlocked = playerData.roleIdList.Contains(roleInfo.id) || roleInfo.lockMoney == 0;
        shopButton.gameObject.SetActive(!isRoleUnlocked);
        sureBtn.gameObject.SetActive(isRoleUnlocked);

        if (!isRoleUnlocked)
        {
            roleMoneyValue.text = "￥" + roleInfo.lockMoney;
        }
        else
        {
            playerData.nowRoleId = roleInfo.id;
        }
        
        characterName.text = roleInfo.name;
    }

    GameObject newRoleObj; 
    private  void   SpawnNewRoleObject(RoleInfo roleInfo)
    {
      GameObject roleGameObject  = Resources.Load<GameObject>(roleInfo.res);
           newRoleObj = Instantiate(roleGameObject);
           newRoleObj.transform.position= rolePosTransform.position;
           roleObj = newRoleObj;
      
    }
 
    public void UpdatePlayerMoneyText(int money)
    {
        moneyValue.text = "￥" + money;
    }

    private void OnDestroy()
    {
        if (roleObj != null)
        {
            Destroy(roleObj);
            GameDataManager.Instance.SaveData(playerData);
        }
        roleList = null;
    }
}
