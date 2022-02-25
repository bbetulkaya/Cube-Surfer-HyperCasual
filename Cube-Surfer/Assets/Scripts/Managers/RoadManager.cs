using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using NaughtyAttributes;

public class RoadManager : MonoBehaviour
{
    private Vector3 currentPosition = Vector3.zero;
    public List<GameObject> roads;

    [BoxGroup("Road Settings")] public GameObject roadPrefab;
    [BoxGroup("Road Settings")] public Transform roadParent;
    [BoxGroup("Road Settings")] public float roadDistance = 5f;
    [BoxGroup("Road Settings"), Dropdown("RoadDirection")] public Vector3 pointDirection;


    [BoxGroup("Final Road Settings")] public GameObject finalRoad;
    [BoxGroup("Final Road Settings")] public Material[] finalMaterials;

#if UNITY_EDITOR
    [Button("Add road")]
    public void AddRoad()
    {
        if (!(roads.Count == 0))
        {
            currentPosition += pointDirection * roadDistance;
        }

        GameObject road = PrefabUtility.InstantiatePrefab(roadPrefab, roadParent) as GameObject;
        road.transform.position = currentPosition;
        road.transform.rotation = Quaternion.Euler(0, pointDirection.x * 90, 0);
        roads.Add(road);

    }

    [Button("Remove road")]
    public void RemoveRoad()
    {
        if (!(roads.Count == 0))
        {
            DestroyImmediate(roads[roads.Count - 1]);
            currentPosition -= pointDirection * roadDistance;
            roads.RemoveAt(roads.Count - 1);

        }
        else
        {
            Debug.Log("Roads is empty!!");
        }
    }

    [Button("Clear Roads")]
    public void ClearAllRoads()
    {
        foreach (GameObject road in roads)
        {
            DestroyImmediate(road);
        }

        roads.Clear();
        currentPosition = Vector3.zero;
    }

    [Button("Create Final Roads")]
    public void CreateFinalRoads()
    {
        AddRoad();
        var firstRoad = roads[roads.Count - 1];
        var firstRenderer = firstRoad.transform.GetComponentInChildren<MeshRenderer>();
        var firstCollider = firstRoad.transform.GetComponentInChildren<BoxCollider>();

        firstRenderer.material = finalMaterials[Random.Range(0, finalMaterials.Length - 1)];

        firstCollider.isTrigger = enabled;
        firstCollider.size = new Vector3(1, 1.5f, 0.5f);
        firstCollider.center = Vector3.back * 2f;
        firstCollider.tag = "FinalGame";

        for (int i = 0; i < 25; i++)
        {
            AddRoad();
            var road = roads[roads.Count - 1];
            var collider = road.transform.GetComponentInChildren<BoxCollider>();
            var renderer = road.transform.GetComponentInChildren<MeshRenderer>();

            renderer.material = finalMaterials[Random.Range(0, finalMaterials.Length - 1)];

            collider.isTrigger = enabled;
            collider.size = new Vector3(1, 1.5f, 0.5f);
            collider.tag = "FinalRoad";
        }
    }

#endif
    private DropdownList<Vector3> RoadDirection()
    {
        return new DropdownList<Vector3>()
        {
            { "X : Right",   Vector3.right },
            { "X : Left",    Vector3.left },
            { "Z : Forward", Vector3.forward },
        };
    }

}
