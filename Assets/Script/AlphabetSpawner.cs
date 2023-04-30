using UnityEngine;

public class AlphabetSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] alphabetPrefabs;
    public bool spawnOnStart = true;
    


    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnAlphabets();
        }
    }

    public void SpawnAlphabets()
    {
        int numAlphabets = alphabetPrefabs.Length;
        for (int i = 0; i < numAlphabets; i++)
        {
            GameObject letterprefabs = alphabetPrefabs[Random.Range(0, numAlphabets)];
            Vector3 spawnPosition = new Vector3(Random.Range(6f,62f), 0.5f, Random.Range(-53f, 141f));
            letterprefabs.tag = "Alpha";
            Collider[] colliders = Physics.OverlapBox(spawnPosition, letterprefabs.transform.localScale/2f);

            if (colliders.Length == 0)
            {
              Instantiate(letterprefabs, spawnPosition, Quaternion.identity).transform.parent=transform;
              
            }
        }
    }
}
