using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public int Loops { get; private set; }

    [SerializeField]
    private List<Color> _values;

    [SerializeField]
    private float _transition = 2f;

    private float _transitionStep;

    private Renderer _myRenderer;

    private Color _currentValue;

    private int _valueIndex;

    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _transitionStep = 0;

        _currentValue = _myRenderer.material.color;

        _valueIndex = 0;

        Loops = 0;
    }

    void Update()
    {

        if (_transition > _transitionStep)
        {
            _transitionStep += Time.deltaTime;

            float step = _transitionStep / _transition;

            _myRenderer.material.color = Color.Lerp(_currentValue, _values[_valueIndex], step);
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
