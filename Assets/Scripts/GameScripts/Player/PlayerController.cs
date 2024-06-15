using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private float rotationSpeed =150;
     Animator animator;
     int atk;
     int money;
     public int Money
    {
        get
        {
            return  money;
        }
        set
        {
            money = value;
            playerData.money = money;
            GameDataManager.Instance.SaveData(playerData);
            UIManagerModule.Instance.GetUIPanel<GamePanel>().UpdatePlayerMoney(money);
        }
    }
     PlayerData playerData;
     RoleInfo roleInfo;
    
    void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
      AddEvent();
        Init();
    }

    private void AddEvent()
    {
        if (PlayerInputManager.Instance!=null)
        {
            
            PlayerInputManager.Instance.OnRoll+=OnRoll;
          PlayerInputManager.Instance.OnSquart+= OnSquart;   
        }
    }

    private void OnRoll()
    {
            animator.SetTrigger("roll");
    }
    private void OnSquart(bool isSquart)
    {
        if (isSquart)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
 
    private void OnMove(Vector2 vector2)
    {
        animator.SetFloat("Y",vector2.x);
        animator.SetFloat("X", vector2.y);
        transform.Rotate(Vector3.up * (rotationSpeed * vector2.x* Time.deltaTime));
    }

    private GameObject eff;
    public  void Init()
    {  animator = GetComponent<Animator>();
        playerData = GameDataManager.Instance.GetData<PlayerData>();
        roleInfo = GameDataManager.Instance.GetData<List<RoleInfo>>("RoleInfoList")[
            playerData.nowRoleId-1
        ];
        if (roleInfo.type == 2)
        {
            muzzleTransform = GameObject.Find("muzzle").transform;
            eff = Resources.Load<GameObject>("hitEff/2");
        }
        money = playerData.money;
        atk = roleInfo.atk;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
        OnMove(PlayerInputManager.Instance.MoveDirection());
        if (queue.Count>0)
        {
            Destroy(queue.Dequeue(),2f);
        }
    }

    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position + transform.forward + transform.up,
            1f,
            1 << LayerMask.NameToLayer("Enemy")
        );
        SoundManager.Instance.PlaySound("Knife");
        foreach (var item in colliders)
        {
          // item.GetComponent<EnemyController>().Hit(atk);
        }
    }

    public Transform muzzleTransform;
    public Queue<GameObject> queue=new Queue<GameObject>();
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(
            muzzleTransform.position-new Vector3(0,0.5f,0),
            muzzleTransform.forward,
            1000f,
             1<<LayerMask.NameToLayer("Enemy")
        );
        SoundManager.Instance.PlaySound("gun");
        foreach (var item in hits)
        {
          //  item.collider.GetComponent<EnemyController>().Hit(atk);
           queue.Enqueue(  Instantiate(eff, item.point, Quaternion.identity));
        }
    }

    private void OnDestroy()
    {
         Instance=null;
    }
}
