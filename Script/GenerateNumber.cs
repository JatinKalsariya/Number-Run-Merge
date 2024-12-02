using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GenerateNumber : MonoBehaviour
{
    [SerializeField] GameObject spawnNumberPrefab;
    [SerializeField] GameObject obstacklePrefab;
    GameObject oldObj;
    BoxCollider boxCollider;
    [SerializeField] Material RedColor;
    private void OnEnable()
    {
        gen();
    }
    public void gen()
    {
        if (oldObj != null)
        {
            Destroy(oldObj);
        }

        if (Random.Range(0, 3) == 0)
        {
            oldObj = Instantiate(obstacklePrefab, transform);
            var randomPos = oldObj.transform.position;
            randomPos.x = Random.Range(-4.5f, 4.5f);
            oldObj.transform.position = randomPos;
        }
        else
        {
            oldObj = Instantiate(spawnNumberPrefab, transform);
            var randomPos = oldObj.transform.position;
            randomPos.x = Random.Range(-4.5f, 4.5f);
            oldObj.transform.position = randomPos;
        }
    }
}