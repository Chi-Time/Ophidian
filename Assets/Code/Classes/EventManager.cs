using System.Collections.Generic;

public class EventManager
{
    public static EventManager Instance
    {
        get
        {
            if (_InstanceInternal == null)
            {
                _InstanceInternal = new EventManager ();
            }

            return _InstanceInternal;
        }
    }

    private static EventManager _InstanceInternal = null;

    public delegate void EventDelegate<T> (T e) where T : GameEvent;
    private delegate void EventDelegate (GameEvent e);

    private Dictionary<System.Type, EventDelegate> _Delegates = new Dictionary<System.Type, EventDelegate> ();
    private Dictionary<System.Delegate, EventDelegate> _DelegatesLookup = new Dictionary<System.Delegate, EventDelegate> ();

    /// <summary> Adds a listener to the event list for the given event type.</summary>
    /// <typeparam name="T">The even to listen for.</typeparam>
    /// <param name="del">The callback to initiate when the event is fired.</param>
    public void AddListener<T> (EventDelegate<T> del) where T : GameEvent
    {
        // Early-out if we've already registered this delegate
        if (_DelegatesLookup.ContainsKey (del))
            return;

        // Create a new non-generic delegate which calls our generic one.
        // This is the delegate we actually invoke.
        EventDelegate internalDelegate = (e) => del ((T)e);
        _DelegatesLookup[del] = internalDelegate;

        EventDelegate tempDel;

        if (_Delegates.TryGetValue (typeof (T), out tempDel))
            _Delegates[typeof (T)] = tempDel += internalDelegate;
        else
            _Delegates[typeof (T)] = internalDelegate;
    }

    /// <summary> Removes a listener from the event list for the given event type.</summary>
    /// <typeparam name="T">The event being listened for.</typeparam>
    /// <param name="del">The callback initiated when the event is triggered.</param>
    public void RemoveListener<T> (EventDelegate<T> del) where T : GameEvent
    {
        EventDelegate internalDelegate;

        if (_DelegatesLookup.TryGetValue (del, out internalDelegate))
        {
            EventDelegate tempDel;

            if (_Delegates.TryGetValue (typeof (T), out tempDel))
            {
                tempDel -= internalDelegate;

                if (tempDel == null)
                    _Delegates.Remove (typeof (T));
                else
                    _Delegates[typeof (T)] = tempDel;
            }

            _DelegatesLookup.Remove (del);
        }
    }

    /// <summary> Raises an event and informs all listeners of the event's info. </summary>
    /// <param name="e">The event to raise.</param>
    public void Raise (GameEvent e)
    {
        EventDelegate del;

        if (_Delegates.TryGetValue (e.GetType (), out del))
            del.Invoke (e);
    }
}

public class GameEvent { }
