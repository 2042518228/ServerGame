using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Scene
{
    BeginScene,
    GameScene1,
    GameScene2,
    GameScene3,
}
    public class SceneLoadingModule:Singleton<SceneLoadingModule>
    {
        public void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public IEnumerator LoadSceneAsync(Scene scene,EventHandler  loadingEvent=null,EventHandler endEvent=null)
        {
            AsyncOperation asyncOperation=SceneManager.LoadSceneAsync(scene.ToString());
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress>=0.9f)
                {
                    loadingEvent?.Invoke(null,EventArgs.Empty);
                }
                yield return null;
              
            }
            endEvent.Invoke(null,EventArgs.Empty);
        }
    }