using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TestAssignment
{
    public class AutoSpaceChildren
    {
        [MenuItem("GameObject/Auto Space Children", false, 11)]
        static void AutoSpace()
        {
            if (Selection.activeGameObject == null)
                return;

            Transform parent = Selection.activeTransform;
            Undo.RegisterFullObjectHierarchyUndo(parent, "Auto Space Children");

            float floorX = 40f;
            float floorZ = 22f;

            float halfX = floorX / 2f;
            float halfZ = floorZ / 2f;

            List<Vector3> placedPositions = new List<Vector3>();

            foreach (Transform child in parent)
            {
                float radius = child.localScale.x * 0.5f;

                Vector3 pos = Vector3.zero;
                bool placed = false;
                
                for (int attempts = 0; attempts < 200; attempts++)
                {
                    float x = Random.Range(-halfX, halfX);
                    float z = Random.Range(-halfZ, halfZ);

                    pos = new Vector3(x, 0f, z);
                    
                    bool overlapped = false;
                    foreach (var existing in placedPositions)
                    {
                        float dist = Vector3.Distance(pos, existing);
                        float neededDist = radius + child.localScale.x * 0.5f;

                        if (dist < neededDist)
                        {
                            overlapped = true;
                            break;
                        }
                    }

                    if (!overlapped)
                    {
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Debug.LogWarning($"Could not place {child.name} without overlap after many attempts.");
                }

                child.localPosition = pos;
                placedPositions.Add(pos);
            }
        }
    }
}