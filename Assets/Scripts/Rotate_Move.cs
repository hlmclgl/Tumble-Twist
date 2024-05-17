using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Move : MonoBehaviour
{
    public float rotateSpeed;
    private float moveX;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float moveX = touch.deltaPosition.x;
                transform.Rotate(0f, moveX * rotateSpeed * Time.deltaTime, 0f);
            }
        }
    }
}
