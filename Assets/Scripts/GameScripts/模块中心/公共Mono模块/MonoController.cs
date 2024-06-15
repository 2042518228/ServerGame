using System;
using UnityEngine;

 
    public class MonoController : SingletonAutoMono<MonoController>
    {
        public event EventHandler OnUpdate;
        public event EventHandler OnStart;
        public void Start()
        {
            OnStart?.Invoke(this,EventArgs.Empty);
        }

        private void Update()
        {
            OnUpdate?.Invoke(this,EventArgs.Empty);
        }
    }
 