using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    //Door Open, Close
    public event Action<int> onDoorwayTriggerEnter;
    public event Action<int> onDoorwayTriggerExit;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region Doors Region
    public void DoorwayTriggerEnter(int id)
    {
        if(onDoorwayTriggerEnter!=null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public void DoorwayTriggerExit(int id)
    {
        if(onDoorwayTriggerExit!=null)
        {
            onDoorwayTriggerExit(id);
        }
    }
    
    #endregion
}
