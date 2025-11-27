using UnityEngine;
using UnityEngine.AI;

public class TerrainTreeObstacleBaker : MonoBehaviour
{
    public Terrain terrain;
    public NavMeshObstacle obstaclePrefab;
    public float radius = 0.5f;
    public float height = 3.0f;
    public float yOffset = 0f;
    public float minHeightScale = 0.6f; // raise/lower until grass is excluded
    public string[] ignoreNameContains = { "grass" }; // optional

    public void Rebuild()
    {
        if (!terrain) terrain = FindFirstObjectByType<Terrain>();
        if (!terrain || !obstaclePrefab) { Debug.LogError("Assign Terrain and Obstacle Prefab."); return; }

        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        var data = terrain.terrainData;
        var prototypes = data.treePrototypes;

        foreach (var ti in data.treeInstances)
        {
            // Skip tiny "tree instances" (often grass)
            if (ti.heightScale < minHeightScale)
                continue;

            // Optional: skip by prototype prefab name
            var prefab = prototypes[ti.prototypeIndex].prefab;
            if (prefab != null)
            {
                string n = prefab.name.ToLower();
                foreach (var s in ignoreNameContains)
                    if (!string.IsNullOrEmpty(s) && n.Contains(s.ToLower()))
                        goto SkipThisTree;
            }

            // --- spawn obstacle ---
            Vector3 world = Vector3.Scale(ti.position, data.size) + terrain.transform.position;
            world.y = terrain.SampleHeight(world) + terrain.transform.position.y + yOffset;

            var ob = Instantiate(obstaclePrefab, world, Quaternion.identity, transform);
            ob.shape = NavMeshObstacleShape.Capsule;
            ob.radius = radius;
            ob.height = height;

            continue;

        SkipThisTree:
            continue;
        }

    }
}

