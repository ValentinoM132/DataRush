using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class EndlessGameManager : MonoBehaviour
{
    public GameObject nextConnection;
    public GameObject[] Prefabs;
    public GameObject endPoint;

    [SerializeField] private NavMeshSurface surface;

    // Call this whenever your level geometry changes
    public void RebuildNavMesh()
    {
        // Rebuilds smoothly without freezing the main thread
        surface.BuildNavMesh();
    }


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject nextPrefab = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], nextConnection.transform.position, Quaternion.identity);
            nextPrefab.transform.rotation = nextConnection.transform.rotation;
            GameObject start = nextPrefab.transform.Find("Start").gameObject;
            start.transform.position = nextConnection.transform.position;
            nextPrefab.transform.parent = nextConnection.transform.parent.parent;
            nextConnection = start.transform.Find("End").gameObject;
            endPoint.transform.position = nextConnection.transform.position;
            endPoint.transform.LookAt(nextConnection.transform.position);
        }
        RebuildNavMesh();
        StartCoroutine(GenerateLevel());
    }

    public IEnumerator GenerateLevel()
    {
        while (true)
        {
            GameObject nextPrefab = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], nextConnection.transform.position, Quaternion.identity);
            nextPrefab.transform.rotation = nextConnection.transform.rotation;
            nextPrefab.transform.Find("Start").position = nextConnection.transform.position;
            nextPrefab.transform.parent = nextConnection.transform.parent.parent;
            nextConnection = nextPrefab.transform.Find("End").gameObject;
            endPoint.transform.position = nextConnection.transform.position;
            endPoint.transform.rotation = nextConnection.transform.rotation;
            RebuildNavMesh();
            yield return new WaitForSeconds(5f); // Wait for 5 seconds before generating the next level piece
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
