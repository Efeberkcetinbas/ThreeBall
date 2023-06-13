using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    OnHitBoss,
    OnBossDie,
    OnBossDieParticle,
    OnSpawnWeapon,
    OnNextLevel,
    OnButtonClicked,
    OnWall,
    OnFingerRelease,
    OnCollectGold,
    OnSaw,
    OnPassBetweenPoints,
    OnSelection,
    OnCharacterSelected

    

}
public class EventManager
{
    private static Dictionary<GameEvent,Action> eventTable = new Dictionary<GameEvent, Action>();
    

    
    public static void AddHandler(GameEvent gameEvent,Action action)
    {
        if(!eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent]=action;
        else eventTable[gameEvent]+=action;
    }

    public static void RemoveHandler(GameEvent gameEvent,Action action)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]-=action;
        if(eventTable[gameEvent]==null)
            eventTable.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]();
    }
}
