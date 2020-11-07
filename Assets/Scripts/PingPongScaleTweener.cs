using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ping pong scale tweener.
/// Interpolates the transform.localScale of a GameObject by doing a single ping-pong with its scale.
/// The loop has two-fixed steps: one for the ping and one for the pong.
/// Keeps a counter to the number of interpolation loops completed since the script started. See <see cref="Loops"/>.
/// </summary>
public class PingPongScaleTweener : BaseTweener<Vector3>
{

    #region Variables

    [SerializeField]
    [Tooltip("Scale multiplier to calculate the object's scale in the middle of the ping-pong interpolation.")]
    private float _scaleMultiplier = 1.5f;

    #endregion

    #region BaseTweener overrides

    protected override Vector3 GetInitialValue()
    {
        return transform.position;
    }

    protected override List<Vector3> GetValues()
    {
        /// Create the list of values from <see cref="_scaleMultiplier"/>.
        var values = new List<Vector3>
        {
            transform.localScale * _scaleMultiplier,
            transform.localScale
        };
        return values;
    }

    protected override void DoLerp(Vector3 startValue, Vector3 endValue, float stepRatio)
    {
        transform.localScale = Vector3.Lerp(startValue, endValue, stepRatio);
    }

    #endregion

}
