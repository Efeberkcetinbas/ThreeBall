using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ThreeBall/WeaponData", order = 3)]
public class WeaponData : ScriptableObject 
{
    public Sprite sprite;
    public int selectedWeaponIndex;
    
}
