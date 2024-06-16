using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel:BaseUIPanel
{
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Slider musicSlider;
    public Slider soundSlider;
    public Button quitBtn;
    public Transform BkTsf;
    public  MusicData musicData;
    protected override void Start()
    {
        base.Start();
    }

    protected override void ComponentAddEvent()
    {
        throw new System.NotImplementedException();
    }

    protected override void FindComponent()
    {
        throw new System.NotImplementedException();
    }


    protected override void Awake()
    {
        alphaSpeed = 2f;
        BkTsf=transform.Find("Bk");
        musicToggle= BkTsf.Find("MusicToggle").GetComponent<Toggle>();
        soundToggle= BkTsf.Find("SoundEffectToggle").GetComponent<Toggle>();
        musicSlider= BkTsf.Find("MusicSlider").GetComponent<Slider>();
        soundSlider= BkTsf.Find("SoundEffectSlider").GetComponent<Slider>();
        quitBtn= BkTsf.Find("QuitBt").GetComponent<Button>();
        musicData=   GameDataManager.Instance.GetData<MusicData>();
        Init();
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        musicToggle.isOn=musicData.isMusicMute;
        musicSlider.value=musicData.musicVolume;
        soundToggle.isOn=musicData.isSoundPlaying;
        soundSlider.value=musicData.soundVolume;
        quitBtn.onClick.AddListener(() =>
        {
           UIManagerModule.Instance.HideUIPanel<SetPanel>();
           GameDataManager.Instance.SaveData(musicData);
        });
        musicToggle.onValueChanged.AddListener((bool isOn) =>
        {
            BkMusicManager.Instance.SetMusicMute(isOn);
             musicData.isMusicMute=isOn;
        });
        musicSlider.onValueChanged.AddListener((float value) =>
        {
            BkMusicManager.Instance.SetMusicVolume(value);
             musicData.musicVolume=value;
        });
        soundToggle.onValueChanged.AddListener((bool isOn) =>
        {
             musicData.isSoundPlaying=isOn;
        });
        soundSlider.onValueChanged.AddListener((float value) =>
        {
          musicData.soundVolume=value;
        });
  
    }

  
}
