#if UNITY_EDITOR
#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor( typeof( Point ), true )]
    public class PointEditor : Editor {

        private Point Target => (Point) target;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
        }

        protected void OnSceneGUI() {
            if (Event.current.type is EventType.MouseDown or EventType.MouseDrag && Event.current.button == 0 && Event.current.control) {
                var ray = HandleUtility.GUIPointToWorldRay( Event.current.mousePosition );
                if (Physics.Raycast( ray, out var hit, 256, ~0, QueryTriggerInteraction.Ignore )) {
                    Undo.RegisterCompleteObjectUndo( Target.transform, null );
                    var point = Snap( hit.point, hit.distance );
                    Target.transform.position = point;
                }
                Target.SendMessage( "OnValidate", null, SendMessageOptions.DontRequireReceiver );
                EditorUtility.SetDirty( Target );
                Event.current.Use();
            }
            if (Event.current.type is EventType.ScrollWheel && Event.current.control) {
                {
                    Undo.RegisterCompleteObjectUndo( Target.transform, null );
                    var delta = Event.current.delta.y > 0 ? 45 : -45;
                    Target.transform.localEulerAngles += new Vector3( 0, delta, 0 );
                }
                Target.SendMessage( "OnValidate", null, SendMessageOptions.DontRequireReceiver );
                EditorUtility.SetDirty( Target );
                Event.current.Use();
            }
            if ((Event.current.isMouse || Event.current.isKey) && Event.current.control) {
                SceneView.RepaintAll();
            }
            if (Event.current.type is EventType.Repaint && Event.current.control) {
                var ray = HandleUtility.GUIPointToWorldRay( Event.current.mousePosition );
                if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                    var point = Snap( hit.point, hit.distance );
                    DrawPoint( point, hit.distance );
                }
            }
        }

        // Helpers
        private static Vector3 Snap(Vector3 point, float distance) {
            if (distance > 10) {
                return Snapping.Snap( point, Vector3.one * 1f );
            }
            return Snapping.Snap( point, Vector3.one * 0.5f );
        }
        private static void DrawPoint(Vector3 point, float distance) {
            var size = distance * 0.1f;
            Handles.DrawLine( point + Vector3.left * size, point + Vector3.right * size, 0 );
            Handles.DrawLine( point + Vector3.forward * size, point + Vector3.back * size, 0 );
        }

    }
}
#endif
