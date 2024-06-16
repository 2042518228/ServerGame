using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BaseUIPanel
{
    private TextMeshProUGUI moneyText;
    private TextMeshProUGUI isWinText;
    private Button quitBt;

    private PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
        Transform bk = transform.Find("Bk");
        moneyText = bk.Find("moneyText").GetComponent<TextMeshProUGUI>();
        quitBt = bk.Find("QuitBt").GetComponent<Button>();
        isWinText = bk.Find("isWinText").GetComponent<TextMeshProUGUI>();
        quitBt.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Scene.BeginScene.ToString());
            UIManagerModule.Instance.HideUIPanel<GamePanel>();
            UIManagerModule.Instance.HideUIPanel(this);
        });
        playerData = GameDataManager.Instance.GetData<PlayerData>();
       
    }

    protected override void ComponentAddEvent()
    {
        throw new NotImplementedException();
    }

    protected override void FindComponent()
    {
        throw new NotImplementedException();
    }

    protected override void Init()
    {
        Cursor.lockState = CursorLockMode.None;
        // 这里不再设置Time.timeScale = 0;
    }

    public void SetPlayerGetMoney(int count)
    {
        playerData.money += count;
        moneyText.text = "￥" + count;
        GameDataManager.Instance.SaveData(playerData);
    }

    public void SetIsWinText(string text)
    {
        isWinText.text = text;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        StartCoroutine(ShowAnimation());
    }

    public override void HideMe(UnityAction action = null)
    {
        base.HideMe(action);
        StartCoroutine(HideAnimation(action));
    }

    private IEnumerator ShowAnimation()
    {
        canvasGroup.alpha = 0;
        isShow = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.unscaledDeltaTime; // 使用Time.unscaledDeltaTime
            yield return null;
        }
        canvasGroup.alpha = 1;
        Time.timeScale = 0; // 在动画完成后暂停游戏
    }

    private IEnumerator HideAnimation(UnityAction action)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        Time.timeScale = 1; // 恢复游戏时间
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.unscaledDeltaTime; // 使用Time.unscaledDeltaTime
            yield return null;
        }
        canvasGroup.alpha = 0;
        action?.Invoke();
    }
}