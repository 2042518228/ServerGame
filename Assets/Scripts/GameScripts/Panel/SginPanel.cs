 
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class SginPanel:BaseUIPanel
    {
         Button signBtn;
         Button logBtn;
         Button quitBtn;
         
         TMP_InputField userNameIF;
         TMP_InputField passWordIF;
      
        protected override void ComponentAddEvent()
        {
           quitBtn.onClick.AddListener(() =>
           {
              DOTweenHide();
              UIManagerModule.Instance.ShowUIPanel<BeginPanel>().DOTweenShow(1f);
           });
           signBtn.onClick.AddListener(() =>
           {
               OnSign();
           });
           logBtn.onClick.AddListener(() =>
           {
               UIManagerModule.Instance.HideUIPanel(this);
               UIManagerModule.Instance.ShowUIPanel<LogPanel>();
           });
        }

        private void OnSign()
        {
            string msg = "";
            if (string.IsNullOrEmpty(userNameIF.text))
            {
                msg += "用户名不能为空\n";
            }
            if (string.IsNullOrEmpty(passWordIF.text))
            {
                msg += "密码不能为空\n";
            }
            if(msg!="")
            {
                UIManagerModule.Instance.ShowUIPanel<TipPanel>().ShowTip(msg);
            }
        }

        protected override void FindComponent()
        {
            Transform bk = transform.Find("Bk");
            signBtn =bk.Find("SignBtn").GetComponent<Button>();
            logBtn = bk.Find("LogBtn").GetComponent<Button>();
            quitBtn = bk.Find("QuitBtn").GetComponent<Button>();
            userNameIF = bk.Find("UserNameIF").GetComponent<TMP_InputField>();
            passWordIF = bk.Find("PassWordIF").GetComponent<TMP_InputField>();
        }

        protected override void Init()
        {
           DOTweenShow(5f);
        }
    }
 