using UnityEngine;

public class RoadRecycler : MonoBehaviour
{
    [SerializeField] GameObject roadPrefab;
    int i;
    void Start()
    {
        var pos = transform.position;
        for (i = 0; i < 10; i++)
        {
            GameObject road = Instantiate(roadPrefab, new Vector3(pos.x, pos.y, pos.z * i), Quaternion.identity, transform);
        }
    }
    void Update()
    {
    }
    public void recycleRoad(GameObject newroad)
    {
        newroad.transform.position = transform.position;
        var newPos = newroad.transform.position;
        newroad.transform.position = new Vector3(newPos.x, newPos.y, newPos.z * i);
        i++;
        var x = newroad.transform.childCount;
        for (int j = 0; j < x; j++)
        {
            var funCall = newroad.transform.GetChild(j).GetComponent<GenerateNumber>();
            funCall.gen();
        }
    }
}