using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Resourse_Depleter : MonoBehaviour
{
    public float Delay;
    public float Amount;

    private bool _deplete;

    [Header("Events")]
    public UnityEvent<float> OnDepleteEvent;

    private float _timer;

    private void Start()
    {
        _deplete = true;
    }

    private void Update()
    {
        if (_timer < Time.time)
        {
            if (_deplete)
                OnDepleteEvent?.Invoke(-Amount);
            else
                _deplete = true;

            _timer = Time.time + Delay;
        }
    }

    public void SkipDeplete()
    {
        _deplete = false;
    }
}
