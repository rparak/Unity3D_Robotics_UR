using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIRawImageEffect : MonoBehaviour
{
    RawImage image;
    Rect rect;

    public float speed = .10f;
    public Vector2 size = new Vector2(5, 5);

    void Update()
    {
        rect.x += Time.deltaTime * speed;
        image.uvRect = rect;
    }

    private void Start()
    {
        image = GetComponent<RawImage>();
        rect = new Rect
        {
            height = size.y,
            width = size.x
        };
    }
}
