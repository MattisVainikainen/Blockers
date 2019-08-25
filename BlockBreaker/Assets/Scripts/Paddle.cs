using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1;
    [SerializeField] float maxX = 15;
    [SerializeField] float ScreenWidthUnits = 16f; 

    void Start()
    {
        
    }

    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * ScreenWidthUnits;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosInUnits, minX, maxX);
        transform.position = paddlePos;
    }
}
