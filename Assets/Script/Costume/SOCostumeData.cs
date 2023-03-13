using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CostumeData", menuName = "ScriptableObjects/Costume",order = 1)]
public class SOCostumeData : ScriptableObject
{
    [SerializeField] List<Define.PartDataColor> headData;
    [HideInInspector] public List<Define.PartDataColor> HeadData=>headData;

    [SerializeField] List<Define.PartDataColor> clothData;
    [HideInInspector] public List<Define.PartDataColor> ClothData => clothData;

    [SerializeField] List<Define.PartDataColor> footData;
    [HideInInspector] public List<Define.PartDataColor> FootData => footData;
}
