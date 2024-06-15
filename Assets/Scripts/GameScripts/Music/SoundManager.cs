 
    using System;
    using UnityEngine;

    public   class SoundManager:MonoBehaviour
    {
        public static SoundManager Instance;
        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public  void PlaySound(string soundRes)
        {
            GameObject game = new GameObject();
          AudioSource audioSource = game.AddComponent<AudioSource>();
          audioSource.volume = GameDataManager.Instance.GetData<MusicData>().soundVolume;
          audioSource.clip = Resources.Load<AudioClip>("Music/"+soundRes);
          audioSource.Play();
          Destroy(game,audioSource.clip.length);
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
 