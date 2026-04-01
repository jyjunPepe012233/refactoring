using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InterfaceRef<>))]
public class InterfaceRefDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var targetProp = property.FindPropertyRelative("target");

		var interfaceType = fieldInfo.FieldType.GetGenericArguments()[0];

		var old = targetProp.objectReferenceValue;

		var obj = EditorGUI.ObjectField(
			position,
			label,
			old,
			typeof(Object),
			true
		);

		if (obj == null)
		{
			targetProp.objectReferenceValue = null;
			return;
		}

		if (interfaceType.IsAssignableFrom(obj.GetType()))
		{
			targetProp.objectReferenceValue = obj;
			return;
		}

		if (obj is GameObject go && go.TryGetComponent(interfaceType, out Component comp))
		{
			targetProp.objectReferenceValue = comp;
			return;
		}

		Debug.LogWarning($"Must implement {interfaceType.Name}");
		targetProp.objectReferenceValue = old;
	}
}