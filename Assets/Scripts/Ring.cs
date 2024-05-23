using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public Transform ball;
    public int ringCount;
    private GameManager gm;

    public float distance = 12.5f;
    private float distanceIncrement = 5f;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if (transform.position.y + distance >= ball.position.y)
        {
            Destroy(gameObject);
            gm.gameScore(25);
            distance += distanceIncrement;
        }
    }
}
