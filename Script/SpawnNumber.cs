using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNumber : MonoBehaviour
{
    [SerializeField] GameObject[] numberList;
    BoxCollider boxCollider;
    [SerializeField] int endValue;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        string SpawnNumber = "" + Random.Range(1, endValue);
        List<float> numPos = GenerateSequence(SpawnNumber.Length);
        boxCollider.size = new Vector3(numPos.Count, 1f, 0.25f);
        gameObject.name = SpawnNumber;
        for (int i = 0; i < SpawnNumber.Length; i++)
        {
            var num = int.Parse(SpawnNumber[i].ToString());
            Instantiate(numberList[num],transform);
            var newPos = transform.position;
            newPos.x += numPos[i];
            transform.GetChild(i).position = newPos;
        }
    }
    public List<float> GenerateSequence(int n)
    {
        List<float> sequence = new List<float>();
        if (n <= 0)
        {
            return sequence;
        }
        float startValue = -((float)(n - 1) / 2);
        float endValue = (float)(n - 1) / 2;
        float stepSize = 1;
        for (float value = startValue; value <= endValue; value += stepSize)
        {
            sequence.Add(value);
        }
        return sequence;
    }
    //character = 5
    //1 middle position = 5
    //offset = 2

}