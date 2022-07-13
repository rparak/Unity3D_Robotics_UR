using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class LeanTweenExtensions
{

    public static LTDescr LeanAlphaText(this TMP_Text textMesh, float to, float time)
    {
        var _color = textMesh.color;
        var _tween = LeanTween
            .value(textMesh.gameObject, _color.a, to, time)
            .setOnUpdate((float _value) => {
                _color.a = _value;
                textMesh.color = _color;
            });
        return _tween;
    }

    public static LTDescr LeanAlphaColor(this Image image, float to, float time)
    {
        var _color = image.color;
        var _tween = LeanTween
            .value(image.gameObject, _color.a, to, time)
            .setOnUpdate((float _value) => {
                _color.a = _value;
                image.color = _color;
            });
        return _tween;
    }
}
