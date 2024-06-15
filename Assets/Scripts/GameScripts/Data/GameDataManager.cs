using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager:Singleton<GameDataManager>
{
  
     public T GetData<T>()where T:new()
     {
        
         return JsonMgr.Instance.LoadData<T>(typeof(T).Name);
     }
     public T GetData<T>(string name)where T:new()
     {
        
         return JsonMgr.Instance.LoadData<T>(name);
     }
     public void SaveData<T>(T t)where T:new()
     {
         JsonMgr.Instance.SaveData(t,typeof(T).Name);
     }
}
