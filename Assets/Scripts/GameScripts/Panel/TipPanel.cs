
using TMPro;
public class TipPanel :BaseUIPanel
{
    public TextMeshProUGUI tipText;
    protected override void Awake()
    {
        base.Awake();
        alphaSpeed=0.5f;
      
    }

    protected override void ComponentAddEvent()
    {
        
    }

    protected override void FindComponent()
    {
        tipText=transform.Find("TipText").GetComponent<TextMeshProUGUI>();
    }

    protected override void Init()
    {
        
    }

 
    public void ShowTip(string tip)
    {
        tipText.text = tip;
        UIManagerModule.Instance.HideUIPanel(this);
    }
}
