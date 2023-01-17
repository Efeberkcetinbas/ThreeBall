using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public int id;

    [SerializeField] private float y1,duration,y2;

    [SerializeField] private GameObject door,button;
    void Start()
    {
        GameEvents.Instance.onDoorwayTriggerEnter+=OnDoorwayOpen;
        GameEvents.Instance.onDoorwayTriggerExit+=OnDoorwayClose;
    }

    private void OnDoorwayOpen(int id)
    {
        if(id==this.id)
        {   
            door.transform.DOLocalMoveY(y1,duration);
            button.transform.DOLocalMoveY(y2,duration);
        }
    }

    private void OnDoorwayClose(int id)
    {
        if(id==this.id)
        {
            //
        }
    }
    
    private void OnDestroy()
    {
        GameEvents.Instance.onDoorwayTriggerEnter-=OnDoorwayOpen;
        GameEvents.Instance.onDoorwayTriggerExit-=OnDoorwayClose;
    }
}
