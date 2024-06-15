 
    using System;
    using Common;
    using UnityEngine;

    public class BaseRequset:MonoBehaviour
    {
        private RequestCode requestCode = RequestCode.None;
        public virtual void Awake()
        {
             GameFacade.Instance.AddRequest(requestCode,this);
        }
        public virtual void OnDestroy()
        {
            GameFacade.Instance.RemoveRequest(requestCode);
        }
        public virtual void OnResponse(string serverMessage)
        {
            
        }
        public virtual void SendRequest(string clientMessage)
        {
            
        }   
    }
 