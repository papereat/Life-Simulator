using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Vector3 Size;
    public Transform DoorTransform;

    public bool isOpen;
    public float TimeToOpen;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale=Size;
        transform.localPosition=new Vector3(Size.x/2,Size.y/2,0);
    }

    // Update is called once per frame
    public override void OnHit(PlayerControler PC)
    {
        if(!isOpen)
        {
            StartCoroutine(Open());
        }   
        else
        {
            StartCoroutine(Close());
        }
    }
    IEnumerator Open()
    {
        float FinishPercent=0;
        float TimeBy=0.05f;
        while(FinishPercent<=1)
        {
            
            DoorTransform.rotation=Quaternion.Euler(0,90*FinishPercent,0);
            FinishPercent=(FinishPercent*TimeToOpen+TimeBy)/TimeToOpen;
            Debug.Log(FinishPercent);
            yield return new WaitForSeconds(TimeBy);
        }
        isOpen=true;
    }
    IEnumerator Close()
    {
        float FinishPercent=0;
        float TimeBy=0.05f;
        while(FinishPercent<1)
        {
            
            
            FinishPercent=(FinishPercent*TimeToOpen+TimeBy)/TimeToOpen;
            DoorTransform.rotation=Quaternion.Euler(0,90*(1-FinishPercent),0);
            Debug.Log(FinishPercent);
            yield return new WaitForSeconds(TimeBy);
        }
        isOpen=false;
    }
}
