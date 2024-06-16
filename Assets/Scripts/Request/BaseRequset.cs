 
    using System;
    using Common;
    using UnityEngine;

    public class BaseRequset:MonoBehaviour
    {
        public RequestCode requestCode = RequestCode.None;
        public ActionCode actionCode = ActionCode.Default;
        public virtual void Awake()
        {
             GameFacade.Instance.AddRequest(actionCode,this);
        }
        public virtual void OnDestroy()
        {
            GameFacade.Instance.RemoveRequest(actionCode);
        }
        public virtual void OnResponse(string serverMessage)
        {
            
        }
        public virtual void SendRequest(string clientMessage)
        {
            GameFacade.Instance.SendRequest(requestCode,actionCode,clientMessage);
        }   
    }
 