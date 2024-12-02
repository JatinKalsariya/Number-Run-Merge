using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class obstackleGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] numberList;
    BoxCollider boxCollider;
    [SerializeField] int endValue;
    //[SerializeField] Material redColor;
    public Color color;
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
            var rr = Instantiate(numberList[num], transform);
            var newPos = transform.position;
            newPos.x += numPos[i];
            transform.GetChild(i).position = newPos;

            rr.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
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
}
