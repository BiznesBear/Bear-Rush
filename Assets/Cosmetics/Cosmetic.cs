using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum type
{
    Hat,
    Glasses,
    AxeSkin
}
[CreateAssetMenu(menuName ="Cosmetic",fileName ="New Cosmetic")]
public class Cosmetic : ScriptableObject
{

    public type CosmeticType;
    public Sprite sprite;
    public float zRotation;
}
