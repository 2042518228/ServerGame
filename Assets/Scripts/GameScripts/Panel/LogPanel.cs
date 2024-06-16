 
    using UnityEngine;
    using UnityEngine.UI;

    public class LogPanel:BaseUIPanel
    {
        
        Button logBtn;
        Button quitBtn;
        InputField userNameIF;
        InputField passWordIF;
        InputField repeatPassWordIF;
      LoginRequest loginRequest;
        protected override void ComponentAddEvent()
        {
            quitBtn.onClick.AddListener(() =>
            {
                UIManagerModule.Instance.HideUIPanel(this);
            });
        }

        protected override void FindComponent()
        {
            Transform bk = transform.Find("Bk");
            loginRequest = transform.GetComponent<LoginRequest>();
            logBtn = bk.Find("LogBtn").GetComponent<Button>();
            quitBtn = bk.Find("QuitBtn").GetComponent<Button>();
            userNameIF = bk.Find("UserNameIF").GetComponent<InputField>();
            passWordIF = bk.Find("PassWordIF").GetComponent<InputField>();
            repeatPassWordIF = bk.Find("RepeatPassWordIF").GetComponent<InputField>();
        }

        protected override void Init()
        {
            
        }
    }
    
 