namespace Module.Core.Systems.Text.Localization
{
    using System.Collections.Generic;

    public class TextData
    {
        private string m_Id;
        private string m_Text;

        public string Id { get => m_Id; }
        public string Text { get => m_Text; }

        public TextData(string id, string text)
        {
            m_Id = id;
            m_Text = text;
        }
    }

    internal class TextGroupInfo
    {
        private int m_GroupID;
        private Dictionary<string, TextData> m_Values = new Dictionary<string, TextData>();

        public int GroupID
        {
            get => m_GroupID;
        }

        public TextGroupInfo(int groupID)
        {
            m_GroupID = groupID;
        }

        public void Add(TextData textInfo)
        {
            m_Values.Add(textInfo.Id, textInfo);
        }

        public TextData Get(string textId)
        {
            TextData result = null;

            m_Values.TryGetValue(textId, out result);

            return result;
        }
    }

    public class TextLocalizer
    {
        public delegate string InterceptTextDelegate(string id, string text);

        private List<TextGroupInfo> m_Groups;
        private Dictionary<string, InterceptTextDelegate> m_InterceptInfos;

        public void AddInterceptInfo(string id, InterceptTextDelegate func)
        {
            if (m_InterceptInfos == null)
            {
                m_InterceptInfos = new Dictionary<string, InterceptTextDelegate>();
            }

            m_InterceptInfos.Add(id, func);
        }

        public void Add(int groupID, string id, string text)
        {
            if (m_Groups == null)
            {
                m_Groups = new List<TextGroupInfo>();
            }

            var findedTG = System.Array.Find<TextGroupInfo>(m_Groups.ToArray(), elem => elem.GroupID == groupID);

            if (findedTG != null)
            {
                findedTG.Add(new TextData(id, text));
            }
            else
            {
                TextGroupInfo gi = new TextGroupInfo(groupID);
                gi.Add(new TextData(id, text));
                m_Groups.Add(gi);
            }
        }

        public void Add(int groupID, List<TextData> textInfos)
        {
            if (m_Groups == null)
            {
                m_Groups = new List<TextGroupInfo>();
            }

            var findedTG = System.Array.Find<TextGroupInfo>(m_Groups.ToArray(), elem => elem.GroupID == groupID);

            if (findedTG != null)
            {
                for (int i = 0, range = textInfos.Count; i < range; ++i)
                {
                    findedTG.Add(textInfos[i]);
                }
            }
            else
            {
                TextGroupInfo gi = new TextGroupInfo(groupID);

                for (int i = 0; i < textInfos.Count; i++)
                {
                    gi.Add(textInfos[i]);
                }

                m_Groups.Add(gi);
            }
        }

        public string GetText(string id)
        {
            if (m_Groups != null)
            {
                foreach (TextGroupInfo info in m_Groups)
                {
                    string val = GetTextValue(info.GroupID, id);

                    if(!string.IsNullOrEmpty(val))
                    {
                        if(m_InterceptInfos != null && m_InterceptInfos.TryGetValue(id, out InterceptTextDelegate value))
                        {
                            return value(id, val);
                        }
                        else
                        {
                            return val;
                        }
                    }
                }
            }

            return id.ToString();
        }

        public string GetText(string id, params object[] args)
        {
            if(m_Groups != null)
            {
                foreach(TextGroupInfo info in m_Groups)
                {
                    string val = GetTextValue(info.GroupID, id, args);

                    if(!string.IsNullOrEmpty(val))
                    {
                        return val;
                    }
                }
            }

            return id.ToString();
        }

        public string GetText(int groupID, string id)
        {
            string val = GetTextValue(groupID, id);

            if(!string.IsNullOrEmpty(val))
            {
                if(m_InterceptInfos != null && m_InterceptInfos.TryGetValue(id,out InterceptTextDelegate value))
                {
                    return value(id, val);
                }
                else
                {
                    return val;
                }
            }

            return id.ToString();
        }

        public string GetText(int groupID, string id, params object[] args)
        {
            string val = GetTextValue(groupID, id, args);

            if(!string.IsNullOrEmpty(val))
            {
                return val;
            }

            return id.ToString();
        }

        private string GetTextValue(int groupID, string id, params object[] args)
        {
            TextGroupInfo info = System.Array.Find<TextGroupInfo>(m_Groups.ToArray(), elem => elem.GroupID == groupID);

            if (info != null)
            {
                TextData textInfo = info.Get(id);
                if (textInfo != null)
                    return System.String.Format(textInfo.Text, args);
            }

            return null;
        }

        private string GetTextValue(int groupID, string id)
        {
            TextGroupInfo info = System.Array.Find<TextGroupInfo>(m_Groups.ToArray(), elem => elem.GroupID == groupID);

            if (info != null)
            {
                TextData textInfo = info.Get(id);

                if (textInfo != null)
                    return textInfo.Text;
            }

            return null;
        }

        public void Clear(int groupID)
        {
            int id = System.Array.FindIndex<TextGroupInfo>(m_Groups.ToArray(), elem => elem.GroupID == groupID);

            if (id >= 0)
            {
                m_Groups.RemoveAt(id);
            }
        }

        public void ClearAll()
        {
            if (m_Groups != null)
            {
                m_Groups.Clear();
            }
        }
    }
}