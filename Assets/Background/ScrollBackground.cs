using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float speed = -2f;
    public float lowerYValue = -10f;
    public float upperYValue = 20f;
    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f); // непрерывное движение вниз по speed умноженное на время?
        if (transform.position.y <= lowerYValue) { // если объект ниже нашей отметки
            transform.Translate(0f, upperYValue, 0f); // объект перемещается на нашу заданную отметку
        }
    }
}
