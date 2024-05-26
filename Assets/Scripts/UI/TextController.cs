using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float score = 0;

    private void Awake()
    {
        //text.text = "Hello World!";   
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        DisplayScore(score);
    }

    private void DisplayScore(float _score)
    {
        _score = _score * 10;
        text.text = _score.ToString("0");
    }
}
