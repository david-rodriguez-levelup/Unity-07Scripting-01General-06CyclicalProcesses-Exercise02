using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Position tweener.
/// Interpolates the transform.position of a GameObject by looping through the list of position steps defined in the inspector.
/// Keeps a counter to the number of interpolation loops completed since the script started. See <see cref="Loops"/>.
/// </summary>
public class PositionTweener : BaseTweener<Vector3>
{

    #region Variables

    [SerializeField]
    [Tooltip("List of ordered position steps to interpolate each loop.")]
    private List<Vector3> _values;

    #endregion

    #region BaseTweener overrides

    protected override Vector3 GetInitialValue()
    {
        return transform.position;
    }

    protected override List<Vector3> GetValues()
    {
        return _values;
    }

    protected override void DoLerp(Vector3 startValue, Vector3 endValue, float stepRatio)
    {
        transform.position = Vector3.Lerp(startValue, endValue, stepRatio);
    }

    #endregion

}
