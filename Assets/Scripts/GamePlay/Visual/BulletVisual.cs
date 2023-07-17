using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVisual : MonoBehaviour
{
    private float angularVel = 0;
    [SerializeField]
    private GameObject model;
    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if(angularVel != 0)
        {
            float deltaAngle = angularVel * Time.deltaTime;
            model.transform.rotation *= Quaternion.AngleAxis(deltaAngle, transform.forward);
        }
    }

    public void UpdateAngularVel(float angularVel)
    {
        this.angularVel = angularVel;
    }

    private void OnDisable()
    {
        model.transform.rotation = Quaternion.identity;
    }
}
