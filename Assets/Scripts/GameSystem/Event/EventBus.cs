using System;
using System.Collections.Generic;

public class EventBus : SingleTon<EventBus>
{
  public delegate void CallBack(Object args);
  Dictionary<EventType, LinkedList<CallBack>> _eventToQueueMap = null;

  public void RegisteTo(EventType eventType, CallBack callBack)
  {
    LinkedList<CallBack> queue = GetOrAddQueue(eventType);
    queue.AddLast(callBack);
  }

  public void UnRegisteTo(EventType eventType, CallBack callBack)
  {
    LinkedList<CallBack> queue = GetQueue(eventType);
    if (queue != null)
    {
      queue.Remove(callBack);
      if (queue.Count <= 0)
      {
        RemoveQueue(eventType);
      }
    }
  }

  public void TriggerEvent(EventType eventType, Object args)
  {
    LinkedList<CallBack> queue = GetQueue(eventType);
    if (queue != null)
    {
      foreach (var handler in queue)
      {
        handler.Invoke(args);
      }
    }
  }

  private LinkedList<CallBack> GetOrAddQueue(EventType eventType)
  {
    LinkedList<CallBack> queue = GetQueue(eventType);
    if (queue == null)
    {
      queue = new LinkedList<CallBack>();
      _eventToQueueMap.Add(eventType, queue);
    }

    return queue;
  }

  private LinkedList<CallBack> GetQueue(EventType eventType)
  {
    if (_eventToQueueMap == null)
    {
      _eventToQueueMap = new Dictionary<EventType, LinkedList<CallBack>>();
    }
    LinkedList<CallBack> queue;
    _eventToQueueMap.TryGetValue(eventType, out queue);
    return queue;
  }

  private void RemoveQueue(EventType eventType)
  {
    LinkedList<CallBack> queue = GetQueue(eventType);
    if (queue != null)
    {
      queue.Clear();
      _eventToQueueMap.Remove(eventType);
    }
  }

  private void OnDestroy()
  {
    if (_eventToQueueMap != null)
    {
      foreach (var key in _eventToQueueMap.Keys)
      {
        _eventToQueueMap[key].Clear();
      }
      _eventToQueueMap.Clear();
      _eventToQueueMap = null;
    }
  }
}
