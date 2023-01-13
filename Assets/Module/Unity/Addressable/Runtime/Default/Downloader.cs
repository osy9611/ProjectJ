namespace Module.Unity.Addressables
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public enum DownloadStatus
    {
        None,
        Failed,
        Ing,
        Complete
    }

    public enum DownloadByLabelsStatus
    {
        None,
        Failed,
        Ing,
        CompleteOneLabel,
        CompleteAll
    }

    public class DownloadInfo
    {
        public long size;
        public long downloadByte;
        public float progress;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class DownloadByLabelsInfo
    {
        public string label;

        public long size;
        public long downloadedByte;
        public float progress;

        public long accumulateDownloadByte;

        public long totalSize;
        public float totalProgress;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public static class Downloader
    {
        static public void GetDownloadSize(List<string> lableKeys, List<string> localizePerfixKeys,System.Action<bool,long> callback)
        {

        }
    }
}