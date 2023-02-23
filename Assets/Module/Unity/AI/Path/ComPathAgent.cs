namespace Module.Unity.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ComPathAgent : MonoBehaviour
    {
        public List<Transform> Path = new List<Transform>();
        [SerializeField] List<Vector3> pathData = new List<Vector3>();
        public List<Vector3> PathData => pathData;

        public Vector3? GetPath(int index)
        {
            if (pathData.Count-1 < index || index == -1)
                return null;

            return pathData[index];
        }

        public Vector3? GetNextPath(int index)
        {
            Vector3? nextPath = GetPath(index);
            if(nextPath == null)
            {
                return null;
            }

            return nextPath;
        }



#if UNITY_EDITOR
        public void GenerateData()
        {
            pathData.Clear();
            for (int i=0,range=Path.Count;i<range;++i)
            {
                pathData.Add(Path[i].position);
            }
        }

        public void ClearPath()
        {
            for (int i = 0, range = Path.Count; i < range; ++i)
            {
                DestroyImmediate(Path[i].gameObject);
            }
            Path.Clear();
        }

        public void ClearPathData()
        {
            pathData.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for (int i = 0, range = pathData.Count-1; i < range; ++i)
            {
                Gizmos.DrawLine(pathData[i], pathData[i + 1]);
            }
        }
#endif
    }

}

