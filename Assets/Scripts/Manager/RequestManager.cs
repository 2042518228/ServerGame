 
    using System.Collections.Generic;
    using Common;
    using UnityEngine;

    public class RequestManager:BaseManager
    {
        Dictionary<ActionCode,BaseRequset> requestDict;
        override public void OnInit()
        {
            requestDict = new Dictionary<ActionCode, BaseRequset>();
        }
        override public void OnDestory()
        {
         requestDict.Clear();   
        }
        public void AddRequest(ActionCode requestCode,BaseRequset baseRequset)
        {
            if (!requestDict.ContainsKey(requestCode))
            {
                requestDict.Add(requestCode,baseRequset);
            }
        }
        public void HandleRequest(ActionCode requestCode,string message)
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
        public void RemoveRequest(ActionCode requestCode)
        {
            if (requestDict.ContainsKey(requestCode))
            {
                requestDict.Remove(requestCode);
            }
        }
    }
 