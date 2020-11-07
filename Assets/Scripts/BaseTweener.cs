using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTweener<T> : MonoBehaviour
{
    public int Loops { get; private set; }

    [SerializeField]
    private float _stepDuration = 2f;

    [SerializeField]
    private float _pauseBetweenLoops = 0f;

    private List<T> _tweenValues;

    private void Start()
    {
        Loops = 0;
        _tweenValues = GetValues();
        StartCoroutine(DoTween());
    }

    private IEnumerator DoTween()
    {
        // Initial values
        int index = 0;
        T startValue = GetInitialValue();
        T endValue = _tweenValues[index];

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
            if (index == _tweenValues.Count)
            {
                Loops++;
                index = 0;
                // If there is a pause between loops, then wait for N seconds 'til resume corroutine.
                if (_pauseBetweenLoops > 0f)
                {
                    yield return new WaitForSeconds(_pauseBetweenLoops);
                }
            }

            // Set endValue for the next step.
            endValue = _tweenValues[index];
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

    protected abstract T GetInitialValue();

    protected abstract List<T> GetValues();

    protected abstract void DoLerp(T currentValue, T nextValue, float stepRatio);

}
