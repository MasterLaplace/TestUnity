using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public int instanceCount = 10;

    void Start()
    {
        for (int i = 0; i < instanceCount; i++)
            Instantiate(objectPrefab, Random.insideUnitSphere * 10, Quaternion.identity);
    }
}
