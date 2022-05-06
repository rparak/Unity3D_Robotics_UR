using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextSizer : MonoBehaviour
{
    public LayoutElement layoutElement;
    public TMP_Text Text;
    public bool ResizeTextObject = true;
    public Vector2 Padding;
    public Vector2 MaxSize = new Vector2(1000, float.PositiveInfinity);
    public Vector2 MinSize;
    public Mode ControlAxes = Mode.Both;

    [Flags]
    public enum Mode
    {
        None = 0,
        Horizontal = 0x1,
        Vertical = 0x2,
        Both = Horizontal | Vertical
    }

    private string _lastText;
    private Mode _lastControlAxes = Mode.None;
    private Vector2 _lastSize;
    public bool _forceRefresh;
    private bool _isTextNull = true;
    private RectTransform _textRectTransform;
    private RectTransform _selfRectTransform;

    protected virtual float MinX
    {
        get
        {
            if ((ControlAxes & Mode.Horizontal) != 0) return MinSize.x;
            return _selfRectTransform.rect.width - Padding.x;
        }
    }
    protected virtual float MinY
    {
        get
        {
            if ((ControlAxes & Mode.Vertical) != 0) return MinSize.y;
            return _selfRectTransform.rect.height - Padding.y;
        }
    }
    protected virtual float MaxX
    {
        get
        {
            if ((ControlAxes & Mode.Horizontal) != 0) return MaxSize.x;
            return _selfRectTransform.rect.width - Padding.x;
        }
    }
    protected virtual float MaxY
    {
        get
        {
            if ((ControlAxes & Mode.Vertical) != 0) return MaxSize.y;
            return _selfRectTransform.rect.height - Padding.y;
        }
    }

    protected virtual void Update()
    {
        if (!_isTextNull && (Text.text != _lastText || _lastSize != _selfRectTransform.rect.size || _forceRefresh || ControlAxes != _lastControlAxes))
        {
            var preferredSize = Text.GetPreferredValues(MaxX, MaxY);
            preferredSize.x = Mathf.Clamp(preferredSize.x, MinX, MaxX);
            preferredSize.y = Mathf.Clamp(preferredSize.y, MinY, MaxY);
            preferredSize += Padding;

            if ((ControlAxes & Mode.Horizontal) != 0)
            {
                _selfRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredSize.x);
                if (ResizeTextObject)
                {
                    _textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredSize.x);

                    if(layoutElement != null)
                    {
                        layoutElement.preferredWidth = _textRectTransform.rect.width;
                    }
                }
            }
            if ((ControlAxes & Mode.Vertical) != 0)
            {
                _selfRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredSize.y);
                if (ResizeTextObject)
                {
                    _textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredSize.y);

                    //Tell Layout Element if one exists
                    if(layoutElement != null)
                    {
                        layoutElement.preferredHeight = _textRectTransform.rect.height;
                    }
                }
            }

            _lastText = Text.text;
            _lastSize = _selfRectTransform.rect.size;
            _lastControlAxes = ControlAxes;
            _forceRefresh = false;
        }
    }

    // Forces a size recalculation on next Update
    public virtual void Refresh()
    {
        _forceRefresh = true;

        _isTextNull = Text == null;
        if (Text) _textRectTransform = Text.GetComponent<RectTransform>();
        _selfRectTransform = GetComponent<RectTransform>();
    }
    private void OnValidate()
    {
        Refresh();
    }
}
