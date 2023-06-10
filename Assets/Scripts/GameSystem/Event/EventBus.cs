using System;
using System.Collections.Generic;

public class EventBus : SingleTon<EventBus>
{
  Dictionary<EventType, LinkedList<IEventHanlder>> _eventToQueueMap = null;

  public void RegisteTo(EventType eventType, IEventHanlder callBackObj)
  {
    LinkedList<IEventHanlder> queue = GetOrAddQueue(eventType);
    queue.AddLast(callBackObj);
  }

  public void UnRegisteTo(EventType eventType, IEventHanlder callBackObj)
  {
    LinkedList<IEventHanlder> queue = GetQueue(eventType);
    if (queue != null)
    {
      queue.Remove(callBackObj);
      if (queue.Count <= 0)
      {
        RemoveQueue(eventType);
      }
    }
  }

  public void TriggerEvent(EventType eventType, Object args)
  {
    LinkedList<IEventHanlder> queue = GetQueue(eventType);
    if (queue != null)
    {
      foreach (var handler in queue)
      {
        handler.Invoke(args);
      }
    }
  }

  private LinkedList<IEventHanlder> GetOrAddQueue(EventType eventType)
  {
    LinkedList<IEventHanlder> queue = GetQueue(eventType);
    if (queue == null)
    {
      queue = new LinkedList<IEventHanlder>();
      _eventToQueueMap.Add(eventType, queue);
    }

    return queue;
  }

  private LinkedList<IEventHanlder> GetQueue(EventType eventType)
  {
    if (_eventToQueueMap == null)
    {
      _eventToQueueMap = new Dictionary<EventType, LinkedList<IEventHanlder>>();
    }
    LinkedList<IEventHanlder> queue;
    _eventToQueueMap.TryGetValue(eventType, out queue);
    return queue;
  }

  private void RemoveQueue(EventType eventType)
  {
    LinkedList<IEventHanlder> queue = GetQueue(eventType);
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
