using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Color tweener.
/// Interpolates the renderer's material.color of a GameObject by looping through the list of color steps defined in the inspector.
/// Keeps a counter to the number of interpolation loops completed since the script started. See <see cref="Loops"/>.
/// Requires the GameObject to have a <see cref="Renderer"/> component.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class ColorTweener : BaseTweener<Color>
{

    #region Variables

    [SerializeField]
    [Tooltip("List of ordered color steps to interpolate each loop.")]
    private List<Color> _values;

    // Renderer component in the GameObject.
    private Renderer _renderer;

    #endregion

    #region Lifecycle

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    #endregion

    #region BaseTweener overrides

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

    #endregion

}
