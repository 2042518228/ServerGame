 
    using System.Collections.Generic;
    using Common;
    using UnityEngine;

    public class RequestManager:BaseManager
    {
        Dictionary<RequestCode,BaseRequset> requestDict;
        override public void OnInit()
        {
            requestDict = new Dictionary<RequestCode, BaseRequset>();
        }
        override public void OnDestory()
        {
         requestDict.Clear();   
        }
        public void AddRequest(RequestCode requestCode,BaseRequset baseRequset)
        {
            if (!requestDict.ContainsKey(requestCode))
            {
                requestDict.Add(requestCode,baseRequset);
            }
        }
        public void HandleRequest(RequestCode requestCode,string message)
        {
            if (requestDict.TryGetValue(requestCode, out BaseRequset baseRequset))
            {
                baseRequset.OnResponse(message);
            }
            else
            {
                Debug.Log("没有找到对应的请求");
            }
        }
        public void RemoveRequest(RequestCode requestCode)
        {
            if (requestDict.ContainsKey(requestCode))
            {
                requestDict.Remove(requestCode);
            }
        }
    }
 