using System;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField]
    private int _loops;

    [SerializeField]
    private float _scaleMultiplier;

    [SerializeField]
    private float _transition;

    private PointsMovement _pointsMovement;

    private bool _scale;

    private Vector3 _currentValue;

    private float _transitionStep;

    private int _steps;

    private float _direction = 1;

    private void Awake()
    {
        _pointsMovement = GetComponent<PointsMovement>();
    }

    private void Start()
    {
        _currentValue = transform.localScale;

        _transitionStep = 0;

        _steps = 0;

        _scale = false;
    }

    void Update()
    {
        if (_pointsMovement.Loops % _loops == 0)
        {
            _scale = true;
        }

        if (_scale && _steps < 1)
        {

            _transitionStep += _direction * Time.deltaTime;

            float step = Math.Min(_transitionStep / _transition, 1);

            transform.localScale = Vector3.Lerp(_currentValue, _currentValue * _scaleMultiplier, step);

            if (step >= 1)
            {
                _direction = -_direction;
            }
            else if (step <= 0)
            {
                _direction = -_direction;

                _scale = false;

                _transitionStep = 0;

                _steps = 0;
            }
        }
    }
}
