//C# Example (LookAtPointEditor.cs)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        //generator.placingObjectMode = GUILayout.Toggle(generator.placingObjectMode, "test");
        GUIStyle style = new GUIStyle();
        style.wordWrap = true;
        EditorGUILayout.TextArea("Key board Instruction:" +  "Use QA, WS, ED to rotation the object. Use =- to changet the radius",
            style);
    }

    public void OnSceneGUI()
    {
        var t = (target as TerrainGenerator);

        //adapted from http://answers.unity3d.com/questions/17141/making-a-tilemap-editor-within-the-unity-editor.html
        // override the mouse down control

        int controlID = GUIUtility.GetControlID(FocusType.Passive);
         if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
            t.OnMouseDown();
         }else if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            t.OnMouseUp();
        }

        if (Event.current.type == EventType.KeyDown)
        {
            if(Event.current.keyCode == KeyCode.Q)
                t.initialRotation = t.initialRotation + Vector3.right;
            else if(Event.current.keyCode == KeyCode.A)
                t.initialRotation = t.initialRotation + Vector3.left;
            else if (Event.current.keyCode == KeyCode.W)
                t.initialRotation = t.initialRotation + Vector3.up;
            else if (Event.current.keyCode == KeyCode.S)
                t.initialRotation = t.initialRotation + Vector3.down;
            else if (Event.current.keyCode == KeyCode.E)
                t.initialRotation = t.initialRotation + Vector3.forward;
            else if (Event.current.keyCode == KeyCode.D)
                t.initialRotation = t.initialRotation + Vector3.back;
            else if (Event.current.keyCode == KeyCode.Equals)
                t.radiusToPutObject = t.radiusToPutObject + 1;
            else if (Event.current.keyCode == KeyCode.Minus)
                t.radiusToPutObject = t.radiusToPutObject - 1;
        }

        if (Event.current.type == EventType.Layout && t.placingObjectEnable)
        {
            HandleUtility.AddDefaultControl(controlID);
         }


        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
        t.MouseRay = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(mousePosition);
        mousePosition.y = -mousePosition.y;

        if(t.CurrentObject == null && t.placingObjectEnable && t.objectsPrefs != null && t.objectsPrefs.Length > 0)
        {
            int ind = Random.Range(0, t.objectsPrefs.Length);
            t.AddNewObject(PrefabUtility.InstantiatePrefab(t.objectsPrefs[ind]) as GameObject);
        }

        t.UpdatePuttingObject();
        

    }
}