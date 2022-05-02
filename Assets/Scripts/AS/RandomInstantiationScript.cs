using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomInstantiationScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float deleteTimer = 10f;
    [SerializeField] private float spawnTimer = 2f;
    private float _spawnTimer;
    private float _deleteTimer;

    private Vector3 GetRandomPoint ()
    {
        var leftPointPosition = leftPoint.position;
        var x = Random.Range(rightPoint.position.x, leftPointPosition.x);
        var y = Random.Range(leftPointPosition.y, leftPointPosition.y - 1);
        var z = Random.Range(leftPointPosition.z - 4, leftPointPosition.z + 4);
        
        return new Vector3(x, y, z);
    }

    private void Awake()
    {
        _spawnTimer = spawnTimer;
        _deleteTimer = deleteTimer;
    }

    private void Update()
    {
        if (_spawnTimer <= 0f)
        {
            var knife = Instantiate(prefabs[0], GetRandomPoint(), Quaternion.Euler(new Vector3(0,-90,-90)));
            Destroy(knife, _deleteTimer);
            _spawnTimer = spawnTimer;
        }

        _spawnTimer -= Time.deltaTime;

        StartCoroutine(FreezeYPositions());
    }

    private IEnumerator FreezeYPositions()
    {
        var knifes = GameObject.FindGameObjectsWithTag("Knife");
        foreach (var knife in knifes)
        {
            if (knife.transform.position.y <= 60)
            {
                Destroy(knife.GetComponent<Rigidbody>());
            }
        }
        yield return null;
    }
}
