using System.Collections.Generic;
using UnityEngine;

public class PointsMovement : MonoBehaviour
{
    public int Loops { get; private set; }

    [SerializeField]
    private List<Vector3> _values;

    [SerializeField]
    private float _transition = 2f;

    private Vector3 _currentValue;

    private float _transitionStep;

    private int _valueIndex;

    private void Start()
    {
        _transitionStep = 0;

        _valueIndex = 0;

        Loops = 0;

        _currentValue = transform.position;
    }

    void Update()
    {
        if (_transition > _transitionStep)
        {
            _transitionStep += Time.deltaTime;

            float step = _transitionStep / _transition;

            transform.position = Vector3.Lerp(_currentValue, _values[_valueIndex], step);
        }
        else
        {
            _transitionStep = 0;

            _currentValue = _values[_valueIndex];

            _valueIndex = (_valueIndex + 1) % _values.Count;

            Loops++;
        }
    }
}
