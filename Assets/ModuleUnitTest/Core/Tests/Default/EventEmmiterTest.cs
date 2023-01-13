using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Module.Core.Systems.Collections.Generic;
using Module.Core.Systems.Events;

public class EventEmmiterTest
{
    public class TestListener
    {
        public void OnChangeValue(IEventArgs arg)
        {
            EventArgs<int>? val = arg as EventArgs<int>?;

            if (val.HasValue)
                Debug.Log(val.Value.Arg1);
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void TestOverView()
    {
        EventEmmiter<int> eventEmmiter = EventEmmiter<int>.Create();
        TestListener listener1 = new TestListener();
        TestListener listener2 = new TestListener();

        eventEmmiter.AddListener(1, listener1.OnChangeValue);
        eventEmmiter.AddListener(1, listener2.OnChangeValue);
        eventEmmiter.Emit(1, new EventArgs<int,int,int>(5,2,3));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EventEmmiterTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
