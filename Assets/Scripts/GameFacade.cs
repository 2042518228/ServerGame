 
    using System;
    using Common;
    using UnityEngine;

    public class GameFacade:MonoBehaviour
    {
        public static GameFacade Instance;
        ClientManager clientManager;
        RequestManager requestManager;
        private void Awake()
        {
            
            if (Instance!=null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        private void Start()
        {
            InitManager();
        }

        private void InitManager()
        {
             clientManager = new ClientManager();
              requestManager = new RequestManager();
              clientManager.OnInit();
              requestManager.OnInit();
        }
        private void DestroyManager()
        {
            clientManager.OnDestory();
            requestManager.OnDestory();
        }
        public void AddRequest( ActionCode requestCode,BaseRequset baseRequset)
        {
            requestManager.AddRequest(requestCode,baseRequset);
        }
        public void RemoveRequest(ActionCode requestCode)
        {
            requestManager.RemoveRequest(requestCode);
        }   
        public void HandleRequest(ActionCode requestCode,string message)
        {
            requestManager.HandleRequest(requestCode,message);
        }
        private void OnDestroy()
        {
            DestroyManager();
        }

        public void SendRequest(RequestCode requestCode, ActionCode actionCode, string clientMessage)
        {
            
        }
    }
     
 