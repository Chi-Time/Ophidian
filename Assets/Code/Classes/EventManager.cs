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

    public void Raise (GameEvent e)
    {
        EventDelegate del;

        if (_Delegates.TryGetValue (e.GetType (), out del))
            del.Invoke (e);
    }
}

public class GameEvent { }
