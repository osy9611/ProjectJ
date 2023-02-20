using Module.Unity.Addressables;
using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Managers : MonoBehaviour
{
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }
    #region Module
    PoolManager pool = new PoolManager();
    ResourceManager resource = new ResourceManager();
    SpriteAtlasManager atlas = new SpriteAtlasManager();
    #endregion

    #region Content
    DataManager data = new DataManager();
    SceneManager scene = new SceneManager();
    ObjectManager objects = new ObjectManager();
    InputManager input = new InputManager();
    AnimationManager ani = new AnimationManager();
    JudgementManager judge = new JudgementManager();
    #endregion


    public static PoolManager Pool { get => Instance.pool; }
    public static ResourceManager Resource { get => Instance.resource; }
    public static SpriteAtlasManager Atlas { get => Instance.atlas; }
    public static DataManager Data { get => Instance.data; }
    public static SceneManager Scene { get => Instance.scene; }
    public static ObjectManager Object { get => Instance.objects; }
    public static InputManager Input { get => Instance.input; }
    public static AnimationManager Ani { get => Instance.ani; }
    public static JudgementManager Judge { get=> Instance.judge; }

    private void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance.pool.Init();

            ComLoader.Create();
            Resource.Init(Pool);
            Data.Init();
        }
    }

    public static void Clear()
    {
        Pool.ClearAll();
        Resource.ReleaseAll();
    }
}