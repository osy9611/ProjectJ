using Module.Unity.Addressables;
using Module.Unity.Core;
using Module.Unity.Custermization;
using Module.Unity.Input;
using Module.Unity.Sound;
using Module.Unity.UGUI;
using Module.Unity.UGUI.Hud;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }
    #region Module
    PoolManager pool = new PoolManager();
    ResourceManager resource = new ResourceManager();
    SpriteAtlasManager atlas = new SpriteAtlasManager();
    CostumeManager costume = new CostumeManager();
    UIManager ui = new UIManager();
    HudManager hud = new HudManager();
    SoundManager sound = new SoundManager();
    InputManager input = new InputManager();
    #endregion

    #region Content
    DataManager data = new DataManager();
    SceneManager scene = new SceneManager();
    ObjectManager objects = new ObjectManager();
    AnimationManager ani = new AnimationManager();
    JudgementManager judge = new JudgementManager();
    MapManager map = new MapManager();
    EffectManager effect = new EffectManager();
    #endregion


    public static PoolManager Pool { get => Instance.pool; }
    public static ResourceManager Resource { get => Instance.resource; }
    public static SpriteAtlasManager Atlas { get => Instance.atlas; }
    public static CostumeManager Costume { get => Instance.costume; }
    public static UIManager UI { get => Instance.ui; }
    public static HudManager Hud { get => Instance.hud; }
    public static SoundManager Sound { get => Instance.sound; }
    public static DataManager Data { get => Instance.data; }
    public static SceneManager Scene { get => Instance.scene; }
    public static ObjectManager Object { get => Instance.objects; }
    public static InputManager Input { get => Instance.input; }
    public static AnimationManager Ani { get => Instance.ani; }
    public static JudgementManager Judge { get => Instance.judge; }
    public static EffectManager Effect { get => Instance.effect; }

    public static MapManager Map { get => Instance.map; }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Object.Execute();
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
            UI.Init(Resource);
            Hud.Init(Resource);
            Sound.Init(Resource);
            Data.Init();
            Effect.Init();
        }
    }

    public static void Clear()
    {
        Resource.Clear();
        Pool.ClearAll();
        UI.Clear();
    }

}