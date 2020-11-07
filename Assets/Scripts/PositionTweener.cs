using System.Collections.Generic;
using UnityEngine;

public class PositionTweener : BaseTweener<Vector3>
{

    [SerializeField]
    private List<Vector3> _values;

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

}
