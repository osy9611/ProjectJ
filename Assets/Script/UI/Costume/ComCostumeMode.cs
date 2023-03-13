using Module.Unity.Custermization;
using Module.Unity.UGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComCostumeMode : UI_Scene
{
    [SerializeField] SOCostumeData costumeData;
    public SOCostumeData CostumeData => costumeData;

    private ComCostumeAgent agent;
    [HideInInspector] public ComCostumeAgent Agent => agent;

    private int[] partCount;

    [SerializeField] Vector3 startPoint;
    public Vector3 StartPoint => startPoint;

    [SerializeField] float rotationY;
    public float RotationzY => rotationY;

    public override void Init()
    {
        base.Init();

        int index = System.Enum.GetNames(typeof(Define.CostumePart)).Length;
        partCount = new int[index];

        agent = FindObjectOfType<ComCostumeAgent>();
    }

    public Color? GetColr(Define.CostumePart part, int index)
    {
        Color? result = null;
        switch (part)
        {
            case Define.CostumePart.Head:
                result = costumeData.HeadData[index].color;
                break;
            case Define.CostumePart.Cloth:
                result = costumeData.ClothData[index].color;
                break;
            case Define.CostumePart.Foot:
                result = costumeData.FootData[index].color;
                break;
        }

        return result;
    }

    public Color? GetColor(Define.CostumePart part, bool isUp)
    {
        Color? result = GetColr(part, partCount[(int)part]);
        int index = (isUp) ? partCount[(int)part]++ : partCount[(int)part]--;
        if (partCount[(int)part] >= costumeData.HeadData.Count)
        {
            partCount[(int)part] = 0;
            Debug.Log(partCount[(int)part]);
        }
        if (partCount[(int)part] < 0)
        {
            partCount[(int)part] = costumeData.HeadData.Count - 1;
        }

        return result;
    }

    public void StartScene(int id)
    {
        bool result = System.Enum.IsDefined(typeof(Define.SceneType), id);
        if (result)
            Managers.Scene.LoadScene((Define.SceneType)id);
        else
            Debug.LogError("scene Number incorrect");
    }
}
