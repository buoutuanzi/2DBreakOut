using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMover : MonoBehaviour
{
    private float collisionHalfWidth = 0;
    private Vector3 _lastFramePos = Vector3.zero;
    public Vector3 LastFramePos {
        get{ return _lastFramePos; } }
    public Vector3 Velocity
    {
        get { return (transform.position - _lastFramePos) / Time.deltaTime; }
    }
    private void Awake()
    {
        collisionHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {
        Vector2 mouseWorldPos = GameUtils.GetMouseWorldPosClampByScreen(collisionHalfWidth / 2, collisionHalfWidth / 2);
        Vector3 oldPos = transform.position;
        Vector3 newPos = new Vector2(mouseWorldPos.x, oldPos.y);
        transform.position = newPos;
        _lastFramePos = oldPos;
    }
}
