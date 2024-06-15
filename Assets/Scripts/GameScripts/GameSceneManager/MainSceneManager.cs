using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private void Awake()
    {
     UIManagerModule.Instance.ShowUIPanel<BeginPanel>();   
    }
    private void Start()
    {
        
    }
}
