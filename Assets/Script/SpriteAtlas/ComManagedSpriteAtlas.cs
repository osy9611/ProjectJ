using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComManagedSpriteAtlas : MonoBehaviour
{
    public bool removeOnDestory = true;
    public List<UnityEngine.U2D.SpriteAtlas> values = new List<UnityEngine.U2D.SpriteAtlas>();
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0, range = values.Count; i < range; i++)
        {
            Managers.Atlas.Add(values[i]);
        }
    }

    public void RemoveAll()
    {
        for (int i = 0, range = values.Count; i < range; i++)
        {
            Managers.Atlas.Remove(values[i]);
        }
    }

    private void OnDestroy()
    {
        RemoveAll();
    }


}