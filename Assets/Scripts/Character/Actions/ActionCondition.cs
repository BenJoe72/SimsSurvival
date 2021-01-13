using UnityEngine;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "ActionCondition", menuName = "ButtonActions/ActionCondition")]
public class ActionCondition : ScriptableObject
{
    public ConditionElement[] conditionElements;

    public bool Evaluate(Manager_Resource resource)
    {
        bool result = conditionElements[0].conneciton == ConditionConnection.And;

        foreach (var element in conditionElements)
        {
            switch (element.conneciton)
            {
                default:
                case ConditionConnection.And:
                    result &= element.Evaluate(resource);
                    break;
                case ConditionConnection.Or:
                    result |= element.Evaluate(resource);
                    break;
            }
        }

        return result;
    }

    [System.Serializable]
    public class ConditionElement
    {
        public Resource_DataDefinition definition;
        public ConditionConnection conneciton;
        public ConditionOperator cOperator;
        public float value;

        public bool Evaluate(Manager_Resource resource)
        {
            var data = resource.data.FirstOrDefault(x => x.definition == definition);

            if (data == null)
                return false;

            bool result = true;
            switch (cOperator)
            {
                default:
                case ConditionOperator.Equals:
                    result &= data.currentValue == value;
                    break;
                case ConditionOperator.LessThan:
                    result &= data.currentValue < value;
                    break;
                case ConditionOperator.GreaterThan:
                    result &= data.currentValue > value;
                    break;
                case ConditionOperator.LessThanEquals:
                    result &= data.currentValue <= value;
                    break;
                case ConditionOperator.GreaterThanEquals:
                    result &= data.currentValue >= value;
                    break;
            }

            return result;
        }
    }
}

public enum ConditionOperator
{
    Equals,
    LessThan,
    GreaterThan,
    LessThanEquals,
    GreaterThanEquals
}

public enum ConditionConnection
{
    And,
    Or
}
