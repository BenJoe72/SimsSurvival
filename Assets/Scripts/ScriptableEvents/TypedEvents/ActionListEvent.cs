using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ActionList Event", menuName = "ScriptableEvents/ActionListEvent")]
public class ActionListEvent : TypedScriptableEvent<List<BaseAction>> { }