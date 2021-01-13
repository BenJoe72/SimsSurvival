using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Resource_Data : MonoBehaviour
{
    public Resource_DataDefinition definition;
    public float currentValue { get; protected set; }

    [Header("Events")]
    public FloatEvent ValueChanged;
    public UnityEvent OnDepleted;
    public UnityEvent OnFilled;
    public UnityEvent OnZero;

    public bool IsFull => currentValue == definition.maxValue;
    public bool IsEmpty => currentValue == definition.minValue;
    public bool IsZero => currentValue == 0f;

    protected void Start()
    {
        Add(definition.startingValue);
    }

    public virtual void Add(float value)
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

    public virtual void DropOffResource(Interaction interaction)
    {
        Resource_Data cdata = interaction.character.Resources.data.FirstOrDefault(x => x.definition.Name == definition.Name);

        if (cdata != null)
            Add(cdata.currentValue);
        else
            Debug.LogError("You are trying to drop off a resource type that you don't have.");
    }

    public void Empty()
    {
        Add(-currentValue);
    }
}
