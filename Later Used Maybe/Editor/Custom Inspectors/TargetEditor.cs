using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Target))]
public class TargetEditor : Editor 
{
	Target myTarget;

	public void OnEnable(){
		myTarget = (Target)target;
	}

	public override void OnInspectorGUI()
	{
		myTarget.m_target_type = (Target.TargetType)EditorGUILayout.EnumPopup("Target Type",myTarget.m_target_type);
		if(myTarget.m_target_type == Target.TargetType.Conveyor){
			myTarget.basket = (GameObject)EditorGUILayout.ObjectField("Basket Pole",myTarget.basket,typeof(GameObject),true);
		}
		if(myTarget.m_target_type == Target.TargetType.FoldedRim){
			myTarget.m_basket_control = (TimedBasketControl)EditorGUILayout.ObjectField("Net Front",myTarget.m_basket_control,typeof(TimedBasketControl),true);
			myTarget.m_folded_time = EditorGUILayout.FloatField("Unfolded Time",myTarget.m_folded_time);
		}
		if(myTarget.m_target_type == Target.TargetType.Windmill){
			myTarget.m_windmill = (RotateObject)EditorGUILayout.ObjectField("Windmill",myTarget.m_windmill,typeof(RotateObject),true);
			myTarget.m_pause_time = EditorGUILayout.FloatField("Pause Time",myTarget.m_pause_time);
		}
		if(myTarget.m_target_type == Target.TargetType.TrapDoor){
			myTarget.m_trapdoor = (GameObject)EditorGUILayout.ObjectField("Trap Door",myTarget.m_trapdoor,typeof(GameObject),true);
			myTarget.m_open_time = EditorGUILayout.FloatField("Open Time",myTarget.m_open_time);
		}
		if(myTarget.m_target_type == Target.TargetType.BonusBlimp){
			myTarget.m_Explosion = (GameObject)EditorGUILayout.ObjectField("Explosion",myTarget.m_Explosion,typeof(GameObject),true);
		}
		EditorUtility.SetDirty(target);
	}
}

//Look at this later. Might need to update unity.
/*[CustomEditor(typeof(Target))]
public class TargetEditor : Editor 
{
	SerializedProperty target_type_prop;
	SerializedProperty basket_prop;
	SerializedProperty basket_control_prop;
	SerializedProperty folded_time_prop;
	SerializedProperty windmill_prop;
	SerializedProperty pause_time_prop;

	public void OnEnable(){
		target_type_prop = serializedObject.FindProperty("m_target_type");
		basket_prop = serializedObject.FindProperty("basket");
		basket_control_prop = serializedObject.FindProperty("m_basket_control");
		folded_time_prop = serializedObject.FindProperty("m_folded_time");
		windmill_prop = serializedObject.FindProperty("m_windmill");
		pause_time_prop = serializedObject.FindProperty("m_pause_time");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		//EditorGUILayout.EnumPopup(target_type_prop);

		if(myTarget.m_target_type == Target.TargetType.Conveyor){
			myTarget.basket = (GameObject)EditorGUILayout.ObjectField("Basket Pole",myTarget.basket,typeof(GameObject),true);
		}
		if(myTarget.m_target_type == Target.TargetType.FoldedRim){
			myTarget.m_basket_control = (TimedBasketControl)EditorGUILayout.ObjectField("Net Front",myTarget.m_basket_control,typeof(TimedBasketControl),true);
			myTarget.m_folded_time = EditorGUILayout.FloatField("Unfolded Time",myTarget.m_folded_time);
		}
		if(myTarget.m_target_type == Target.TargetType.Windmill){
			myTarget.m_windmill = (RotateObject)EditorGUILayout.ObjectField("Windmill",myTarget.m_windmill,typeof(RotateObject),true);
			myTarget.m_pause_time = EditorGUILayout.FloatField("Pause Time",myTarget.m_pause_time);
		}
		serializedObject.ApplyModifiedProperties();
	}
}*/