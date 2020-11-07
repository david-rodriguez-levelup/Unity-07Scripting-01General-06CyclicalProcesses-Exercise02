using System.Collections.Generic;
using UnityEngine;

public class ColorTweener : BaseTweener<Color>
{

    [SerializeField]
    private List<Color> _values;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    protected override Color GetInitialValue()
    {
        return _renderer.material.color;
    }

    protected override List<Color> GetValues()
    {
        return _values;
    }

    protected override void DoLerp(Color startValue, Color endValue, float stepRatio)
    {
        _renderer.material.color = Color.Lerp(startValue, endValue, stepRatio);
    }

}
