using Common;
using UnityEngine;

 
    public class LoginRequest :BaseRequset
    {
        public override void Awake()
        {
            base.Awake();
            actionCode = ActionCode.Login;
            requestCode = RequestCode.User;
        }

        public  void SendRequest(string userName,string passWord)
        {
            base.SendRequest(userName + "," + passWord);
        }
    }
 