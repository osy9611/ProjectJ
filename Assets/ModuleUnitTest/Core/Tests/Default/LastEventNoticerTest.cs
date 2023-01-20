using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Module.Core.Systems.Collections.Generic;
using Module.Core.Systems.Events;

public class LastEventNoticerTest
{
    public class ComTestListener : MonoBehaviour, IEventNoticeListener
    {
        private LastEventNoticer<int> m_EventNoticer;

        public LastEventNoticer<int> EventNoticer
        {
            get => m_EventNoticer;
            set => m_EventNoticer = value;
        }

        private void OnEnable()
        {
            m_EventNoticer.Verify(1, this);
        }

        public void VerifyNotice()
        {
            m_EventNoticer.Verify(1, this);
        }

        public bool ReceivableFromEvent()
        {
            return gameObject.activeSelf;
        }

        public void OnChangeValue(IEventArgs arg)
        {
            Debug.Log(name + " : OnChangeValue");

            EventArgs<int>? val = arg as EventArgs<int>?;

            if (val.HasValue)
            {
                Debug.Log(val.Value.Arg1);
            }
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void LastEventNoticerTestSimplePasses()
    {
        LastEventNoticer<int> eventNoticer = LastEventNoticer<int>.Create();

        GameObject g1 = new GameObject("g1");
        var gl1 = g1.AddComponent<ComTestListener>();
        gl1.EventNoticer = eventNoticer;    
        
        GameObject g2 = new GameObject("g2");
        var gl2 = g2.AddComponent<ComTestListener>();
        gl2.gameObject.SetActive(false);
        gl2.EventNoticer = eventNoticer;

        eventNoticer.AddListener(1, gl1, gl1.OnChangeValue);
        eventNoticer.AddListener(1, gl2, gl2.OnChangeValue);

        eventNoticer.Notify(1, new EventArgs<int>(5));

        //Ȱ��ȭ ���� �� �˸��� �ִ��� Ȯ���Ѵ�.
        //SetActive(true); �� ���� OnEnable ȣ���� Editor���� �Ұ��� �׷��� ���� VerifyNotice�� Ȯ��
        gl1.VerifyNotice();
        gl2.VerifyNotice();
        gl2.VerifyNotice();

        gl1.gameObject.SetActive(false);
        gl2.gameObject.SetActive(true);

        eventNoticer.Notify(1, new EventArgs<int>(10));
        eventNoticer.Notify(1, new EventArgs<int>(15));

        gl1.gameObject.SetActive(false);

        if(gl1.ReceivableFromEvent())
        {
            gl1.VerifyNotice();
        }

        gl1.VerifyNotice();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LastEventNoticerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
