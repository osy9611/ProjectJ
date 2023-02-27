namespace Module.Unity.AI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class PathInfo
    {
        [SerializeField] int id;
        public int Id => id;

        [SerializeField] List<Transform> path = new List<Transform>();
        public List<Transform> Path => path;

        [SerializeField] List<Vector3> pathData = new List<Vector3>();
        public List<Vector3> PathData => pathData;

        public bool ShowPath = false;

        public Vector3? GetPath(int index)
        {
            if (pathData.Count - 1 < index || index == -1)
                return null;

            return pathData[index];
        }
        

        public void GenerateData()
        {
            pathData.Clear();
            for (int i = 0, range = path.Count; i < range; ++i)
            {
                pathData.Add(path[i].position);
            }
        }

        public void ClearPath()
        {
            for (int i = 0, range = path.Count; i < range; ++i)
            {
                UnityEngine.Object.DestroyImmediate(path[i].gameObject);
            }
            path.Clear();
        }

        public void ClearPathData()
        {
            pathData.Clear();
        }

    }

    public class ComPathAgent : MonoBehaviour
    {
        [SerializeField] List<PathInfo> pathInfo = new List<PathInfo>();
        public List<PathInfo> PathInfo => pathInfo;

        public PathInfo GetPath(int id)
        {
            PathInfo info = pathInfo.Find(x => x.Id == id);
            return info;
        }


#if UNITY_EDITOR
        public void GenerateData()
        {
            for (int i = 0, range = pathInfo.Count; i < range; ++i)
            {
                pathInfo[i].GenerateData();
            }
        }

        public void ClearPath()
        {
            for (int i = 0, range = pathInfo.Count; i < range; ++i)
            {
                pathInfo[i].ClearPath();
            }
        }

        public void ClearPathData()
        {
            for (int i = 0, range = pathInfo.Count; i < range; ++i)
            {
                pathInfo[i].ClearPathData();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            foreach (var info in pathInfo)
            {
                if (!info.ShowPath)
                    continue;

                Debug.Log(info.Path.Count);
                if(info.Path.Count != 0)
                {
                    for (int i = 0, range = info.Path.Count - 1; i < range; ++i)
                    {
                        Gizmos.DrawLine(info.Path[i].position, info.Path[i + 1].position);
                    }
                }
                else
                {
                    for (int i = 0, range = info.PathData.Count - 1; i < range; ++i)
                    {
                        Gizmos.DrawLine(info.PathData[i], info.PathData[i + 1]);
                    }
                }
            }
        }
#endif
    }

}

