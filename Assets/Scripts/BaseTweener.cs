using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all step-based tweeners.
/// Starts a corroutine that helps with the execution of lerp functions in the subclasses.
/// Keeps a counter with the number of loops completed since the script started. See <see cref="Loops"/>.
/// Each subclass is responsible to return its list of values to interpolate. See <see cref="GetSteps"/>
/// A step is defined by an interpolation between to values.
/// A loop is completed each time the list of steps is traversed.
/// <see cref="ColorTweener"/>.
/// <see cref="PositionTweener"/>.
/// <see cref="PingPongScaleTweener"/>.
/// </summary>
public abstract class BaseTweener<T> : MonoBehaviour
{

    #region Variables

    /// <summary>
    /// Number of loops completed since the script started.
    /// A loop is completed by going through all the steps in the list managed by the subclass.
    /// </summary>
    public int Loops { get; private set; }

    [SerializeField]
    [Tooltip("Duration of each step (seconds).")]
    private float _stepDuration = 2f;

    [SerializeField]
    [Tooltip("Pause between loops (seconds).")]
    private float _pauseBetweenLoops = 0f;

    /// A reference to the list of values returned in <see cref="GetSteps"/> by the subclasses.
    private List<T> _values;

    #endregion

    #region Lifecycle

    private void Start()
    {
        Loops = 0;
        _values = GetSteps();
        StartCoroutine(DoTween());
    }

    private IEnumerator DoTween()
    {
        // Initial values
        int index = 0;
        T startValue = GetInitialValue();
        T endValue = _values[index];

        // Let's tween forever!
        while (true)
        {
            // Do next step! - A step is a lerp between two values.
            yield return StartCoroutine(DoTweenStep(startValue, endValue));

            // Set startValue for the next step.
            startValue = endValue;

            // Increase index.
            index++;

            // Is current loop done?
            if (index == _values.Count)
            {
                Loops++;
                index = 0;
                // If there is a pause between loops, then wait for N seconds 'til resume coroutine.
                if (_pauseBetweenLoops > 0f)
                {
                    yield return new WaitForSeconds(_pauseBetweenLoops);
                }
            }

            // Set endValue for the next step.
            endValue = _values[index];
        }
    }

    private IEnumerator DoTweenStep(T startValue, T endValue)
    {
        float stepTimeElapsed = 0;
        while (stepTimeElapsed < _stepDuration)
        {
            stepTimeElapsed += Time.deltaTime;
            float stepRatio = stepTimeElapsed / _stepDuration;
            DoLerp(startValue, endValue, stepRatio);
            yield return null;
        }
    }

    #endregion

    #region Abstract methods

    /// <summary>
    /// Subclasses must override this method to return the initial value (used for the first interpolation only).
    /// </summary>
    /// <return>Initial value (used for the first interpolation only).</return>
    protected abstract T GetInitialValue();

    /// <summary>
    /// Subclasses must override this method to return their own list of steps (values to interpolate in a loop).
    /// </summary>
    /// <return>List of steps (values to interpolate in a loop).</return>
    protected abstract List<T> GetSteps();

    /// <summary>
    /// Subclasses must override this method to manage the lerp from currentValue to nextValue.
    /// </summary>
    /// <param name="currentValue">Start value of the interpolation.</param>
    /// <param name="nextValue">End value of the interpolation.</param>
    /// <param name="stepRatio">Parameter (t) of the interpolation in the closed unit interval [0, 1].</param>
    protected abstract void DoLerp(T currentValue, T nextValue, float stepRatio);

    #endregion

}
