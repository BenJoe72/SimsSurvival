using UnityEngine;

public class Resource_Data : MonoBehaviour
{
    public Resource_DataDefinition definition;
    public float currentValue { get; protected set; }

    [Header("Events")]
    public FloatEvent ValueChanged;
    public ScriptableEvent OnDepleted;
    public ScriptableEvent OnFilled;
    public ScriptableEvent OnZero;

    public bool IsFull => currentValue == definition.maxValue;
    public bool IsEmpty => currentValue == definition.minValue;
    public bool IsZero => currentValue == 0f;

    protected void Start()
    {
        Add(definition.startingValue);
    }

    public void Add(float value)
    {
        currentValue = Mathf.Clamp(currentValue + value, definition.minValue, definition.maxValue);

        if (IsEmpty)
            OnDepleted?.Invoke();

        if (IsFull)
            OnFilled?.Invoke();

        if (IsZero)
            OnZero?.Invoke();

        ValueChanged?.Invoke(currentValue);
    }
}
