using UnityEngine;
using System.Collections;

public class Resourse_Depleter : MonoBehaviour
{
    public float Rate;
    public float Amount;

    [Header("Events")]
    public FloatEvent OnDepleteEvent;

    private float _timer;

    private void Update()
    {
        if (_timer < Time.time)
        {
            OnDepleteEvent?.Invoke(-Amount);
            _timer = Time.time + (1 / Rate);
        }
    }
}
