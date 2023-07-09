using UnityEngine;

public class DeadZone : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    IReuseableItem reuseable = other.gameObject.GetComponent<IReuseableItem>();
    if (reuseable != null)
    {
       reuseable.Return();
    }
  }
}
