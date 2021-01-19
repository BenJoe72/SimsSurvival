using UnityEngine;

public class Resource_StatData : Resource_Data
{
    public FloatEvent SelectedValueChanged; // Used for only broadcast when character is selected
    private bool _selected;

    public override void Add(float value)
    {
        base.Add(value);

        if (_selected)
            SelectedValueChanged?.Invoke(currentValue);
    }

    public void Select()
    {
        _selected = true;
        SelectedValueChanged?.Invoke(currentValue);
    }

    public void Deselect()
    {
        _selected = false;
    }
}
