using UnityEngine;

public class FollowTarget : MonoBehaviour
{
  Transform _targetTransform;

  private void LateUpdate()
  {
    if (_targetTransform != null)
    {
      transform.position = _targetTransform.position;
    }
  }

  public void Follow(Transform targetTransform)
  {
    _targetTransform = targetTransform;
  }
}
