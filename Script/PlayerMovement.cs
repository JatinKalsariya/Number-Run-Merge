using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject[] numberList;
    bool count = false, gameStarted = false;
    BoxCollider boxCollider;
    public GameObject Loose;
    [SerializeField] Text highScore;
    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obj")
        {
            yield return new WaitForSeconds(4);
            GameObject newRoad = other.transform.gameObject;
            newRoad.SetActive(false);
            var funCall = other.transform.parent.GetComponent<RoadRecycler>();
            newRoad.SetActive(true);
            funCall.recycleRoad(newRoad);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Number")
        {
            int a = int.Parse(other.gameObject.name);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + a);
            numGen();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            int a = int.Parse(other.gameObject.name);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - a);
            if (PlayerPrefs.GetInt("Score") > 0)
            {
                numGen();
            }
            else
            {
                Destroy(transform.GetChild(0).gameObject);
                Loose.SetActive(true);
            }
            Destroy(other.gameObject);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("Score", 1);
        rb = GetComponent<Rigidbody>();
        boxCollider = rb.GetComponent<BoxCollider>();
    }
    private void LateUpdate()
    {
        if (PlayerPrefs.GetInt("Score") > 0)
        {
            setChild();
        }
    }
    void Update()
    {
        var scoreCounter = PlayerPrefs.GetInt("Score");
        if ( scoreCounter > 0 ) {
            gameStarted = true;
            float mouseAxis = Input.GetAxis("Mouse X") * -1;
            if (  gameStarted == true && Input.GetKey(KeyCode.Mouse0)) {
                rb.velocity = Vector3.forward * 10;
                var pos = transform.position;
                pos.x = Mathf.Clamp(pos.x - mouseAxis, -4f, 4f);
                transform.position = pos;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            count = gameStarted =  false;
        }
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("highScore")) {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("Score"));
        }
        highScore.text = PlayerPrefs.GetInt("highScore") + "";
    }
    void numGen()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        int b;
        string a = PlayerPrefs.GetInt("Score") + "";
        List<float> numPos = GenerateSequence(a.Length);
        boxCollider.size = new Vector3(numPos.Count, 1f, 0.25f);
        for (int i = 0; i < a.Length; i++)
        {
            b = int.Parse(a[i] + "");
            var numberrr = Instantiate(numberList[b], transform);
            var newPos = transform.position;
            newPos.x += numPos[i];
            numberrr.transform.position = newPos;
            doAnimation(numberrr.transform, i);
        }
    }
    void setChild()
    {
        string a = PlayerPrefs.GetInt("Score") + "";
        List<float> numPos = GenerateSequence(a.Length);
        boxCollider.size = new Vector3(numPos.Count, 1f, 0.25f);
        for (int i = 0; i < a.Length; i++)
        {
            var numberrr = transform.GetChild(i);
            var newPos = transform.position;
            newPos.x += numPos[i];
            newPos.y = numberrr.transform.position.y;
            numberrr.transform.position = newPos;
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
    void doAnimation(Transform number, int i)
    {
        var newPos = number.position;
        number.transform.DOJump(newPos, 2.5f, 1, 0.2f).SetDelay(.05f * i);
    }
    public void onClickTryAgain()
    {
        SceneManager.LoadScene("PlayingScene");
    }
}