using System.Collections.Generic;
using UnityEngine;

public class PingPongScaleTweener : BaseTweener<Vector3>
{

    [SerializeField]
    private float _scaleMultiplier = 1.5f;

    protected override Vector3 GetInitialValue()
    {
        return transform.position;
    }

    protected override List<Vector3> GetValues()
    {
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

}
