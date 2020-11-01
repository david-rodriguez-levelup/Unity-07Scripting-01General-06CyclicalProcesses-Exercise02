using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Color tweener.
/// Interpolates the transform.localScale of a GameObject by doing a single ping-pong with its scale.
/// The loop has two-fixed steps: one for the ping and one for the pong.
/// Keeps a counter to the number of interpolation loops completed since the script started. See <see cref="Loops"/>.
/// </summary>
public class PingPongScaleTweener : BaseTweener // TODO: Rename to ColorTweener!
{

    #region Fields/Properties

    [SerializeField]
    [Tooltip("Scale multiplier to calculate the object's scale in the middle of the ping-pong interpolation.")]
    private float _scaleMultiplier = 1.5f;
    
    // List to store the ping-pong interpolation values on awake.
    private List<Vector3> _steps;

    // Initial scale of the object. Needed for the initial lerp.
    private Vector3 _initialScale;

    #endregion

    #region Lifecycle

    private void Awake()
    {
        _initialScale = transform.localScale;
        _steps = new List<Vector3>();
        _steps.Add(_initialScale * _scaleMultiplier); // Ping!
        _steps.Add(_initialScale); // Pong!
    }

    # endregion  

    #region BaseTweener overrides

    protected override int GetNumSteps() 
    {
        return _steps.Count;
    }

    protected override void DoInitialLerp(float stepRatio) 
    {
        transform.localScale = Vector3.Lerp(_initialScale, _steps[0], stepRatio);
    }

    protected override void DoLerp(int startIndex, int endIndex, float stepRatio)
    {
        transform.localScale = Vector3.Lerp(_steps[startIndex], _steps[endIndex], stepRatio);
    }  

    #endregion

}
