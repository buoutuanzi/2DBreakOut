using System;
using System.Collections.Generic;
public abstract class View
{
  protected Controller _controller;
  // ����ʱ��ʼ����ֻ�����һ��
  public abstract void Init();
  // �ؿ��л�ʱ���³�ʼ�������ε���
  public abstract void Reset();
  public abstract void OnDestroy();
  public void BindController(Controller controller)
  {
    _controller = controller;
  }

  public abstract void OnDataChange(Dictionary<Enum, object> changedData);
}
