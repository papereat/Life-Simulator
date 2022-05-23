using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public Vector2 MouseSencitivity;
    public Rigidbody RB;
    


    [Header("Interaction")]
    public float InteractionDistance;
    public Transform CameraTransform;
    public LayerMask LM;

    [Header("Hunger")]
    public float Hunger;
    public float HungerDrainRate;

    [Header("Inventory")]
    public float MaxSizePerHand;
    public float MaxWeightPerHand;
    public List<Item> RightHandInventory;
    public List<Item> LeftHandInventory;

    //Add Item to Hand
    public void AddItemTo(bool RightHand, Item add)
    {
        if(RightHand)
        {
            if(CanAdd(RightHandInventory,add))
            {
                RightHandInventory.Add(add);
            }
        }
        else
        {
            if(CanAdd(LeftHandInventory,add))
            {
                LeftHandInventory.Add(add);
            }
        }
    }
    //Check if Item Can Be Added 
    public bool CanAdd(List<Item> AddTo,Item Add)
    {
        float CurrentWeight=0;
        float CurrentSize=0;
        foreach (var item in AddTo)
        {
            CurrentWeight+=item.Weight;
            CurrentSize+=item.Size;
        }
        if(CurrentWeight+Add.Weight<=MaxWeightPerHand && CurrentSize+Add.Size<=MaxSizePerHand)
        {
            return true;
        }
        return false;
    }

    //Eat Function
    public void Eat(food toEat)
    {
        Hunger+=toEat.FoodValue;
    }
    //Hunger function
    IEnumerator HungerFunction()
    {
        while (true)
        {
            Hunger-=HungerDrainRate/60;
            yield return new WaitForSeconds(1);
        }
    }

    //Movement Function
    IEnumerator Movement()
    {
        Vector2 RotateAmount=new Vector2();
        float WaitTime=0.01f;
        while (true)
        {
            //Mouse
            float MouseMovX=Input.GetAxisRaw("Mouse X")*MouseSencitivity.x*WaitTime;
            float MouseMovY=Input.GetAxisRaw("Mouse Y")*MouseSencitivity.y*WaitTime;
            RotateAmount.x+=MouseMovY;
            RotateAmount.y+=MouseMovX;
            RotateAmount.x=Mathf.Clamp(RotateAmount.x,-90f,90f);
            
            transform.rotation= Quaternion.Euler(0,RotateAmount.y,0);
            CameraTransform.localRotation= Quaternion.Euler(RotateAmount.x,0,0);

            
            //wasd
            Vector3 mov=new Vector3(0,0);
            if(Input.GetKey(KeyCode.W))
            {
                mov+=transform.TransformDirection(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.S))
            {
                mov+=-transform.TransformDirection(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.D))
            {
                mov+=transform.TransformDirection(Vector3.right);
            }
            if(Input.GetKey(KeyCode.A))
            {
                mov+=-transform.TransformDirection(Vector3.right);
            }
            RB.velocity=Speed*mov;

            


            yield return new WaitForSeconds(WaitTime);
        }
    }

    //Interaction Function
    void  CheckInteract()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(CameraTransform.position,CameraTransform.TransformDirection(Vector3.forward),out hit,InteractionDistance,LM))
        {
            hit.collider.gameObject.GetComponent<Interactable>().OnHit(this);
            Debug.Log("Hit");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Start Coroutines
        StartCoroutine(Movement());
        StartCoroutine(HungerFunction());

        //Mouse stuff
        Cursor.lockState =CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        //interacr Checker
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckInteract();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            foreach (var item in RightHandInventory)
            {
                if(item.ItemType=="Food")
                {
                    Eat((food)item);
                    RightHandInventory.Remove(item);
                    break;
                }
            }
        }


        //Debug
        Debug.DrawRay(CameraTransform.position,CameraTransform.TransformDirection(Vector3.forward) * InteractionDistance, Color.white);
        
    }
}
