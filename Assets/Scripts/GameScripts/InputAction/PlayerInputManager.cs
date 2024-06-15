
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;
    
   public event UnityAction  OnRoll;
   public event UnityAction<bool> OnSquart;
   public event UnityAction OnFort1;
   public event UnityAction OnFort2;
   public event UnityAction OnFort3;
   public event UnityAction OnFort4;
   public event UnityAction OnQuit;
 
   private InputControls inputControls;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
         inputControls= new InputControls();
        inputControls.Enable();
        inputControls.Player.Roll.performed+= RollOnperformed;
   
        inputControls.Player.Squart.performed+= SquartOnperformed;
        inputControls.Player.Squart.canceled+= SquartOncanceled;
        inputControls.Player.Fort1.performed+= Fort1Onperformed;
        inputControls.Player.Fort2.performed+= Fort2Onperformed;
        inputControls.Player.Fort3.performed+= Fort3Onperformed;
        inputControls.Player.Fort4.performed+= Fort4Onperformed;
        inputControls.Player.Quit.performed+= QuitOnperformed;
    }

    private void QuitOnperformed(InputAction.CallbackContext obj)
    {
        OnQuit?.Invoke();
    }

    private void SquartOncanceled(InputAction.CallbackContext obj)
    {
        OnSquart?.Invoke(false);
    }

    private void Fort1Onperformed(InputAction.CallbackContext obj)
    {
        OnFort1?.Invoke();
    }
    private void Fort2Onperformed(InputAction.CallbackContext obj)
    {
      OnFort2?.Invoke();    
    } 
    private void Fort3Onperformed(InputAction.CallbackContext obj)
    {
        OnFort3?.Invoke();
    }
    private void Fort4Onperformed(InputAction.CallbackContext obj)
    {
        OnFort4?.Invoke();
    }
    private void SquartOnperformed(InputAction.CallbackContext obj)
    {
        OnSquart?.Invoke(true);
    }

    private void RollOnperformed(InputAction.CallbackContext obj)
    {
        OnRoll?.Invoke();
    }

    public Vector2 MoveDirection() 
    {
     return inputControls.Player.Move.ReadValue<Vector2>();
    }
    private void OnDestroy()
    {
        Instance=null;
    }
}
