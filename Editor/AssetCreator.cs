using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;

public class AssetCreator {

	public const String MENU = "Assets/Create/";

	[MenuItem(MENU + "From Selected")]
	public static void CreateAssetOfSelected() {
		try {
			MonoScript script = Selection.activeObject as MonoScript;

			Type type = script.GetClass();

			MethodInfo method = typeof(AssetCreator).GetMethod("CreateAsset", BindingFlags.Public | BindingFlags.Static);
			MethodInfo generic = method.MakeGenericMethod(type);

			generic.Invoke(null, null);
		} catch (Exception e) {
			Debug.Log("Couldn't create asset of selected object.\n" + e.Message);
		}
	}

	// easily add specific creators:
	/*[MenuItem(MENU + "My Asset")]
	public static void CreateMyAsset() {
		CreateAsset<MyAsset>();
	}*/

	public static void CreateAsset<T> () where T : ScriptableObject {
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "") {
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "") {
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

}
