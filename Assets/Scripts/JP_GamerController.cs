using UnityEngine;
using TMPro; 

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timeText; 
    public float timeCount = 30f;    // tempo inicial em segundos
    private bool timeOver = false;

    void Update()
    {
        if (timeOver || timeCount <= 0f)
            return;

        timeCount -= Time.deltaTime;
        timeCount = Mathf.Max(timeCount, 0f); // impede valor negativo

        timeText.text = "Time: " + Mathf.CeilToInt(timeCount).ToString();

        if (timeCount <= 0f)
        {
            timeOver = true;
            Debug.Log("Tempo acabou!");
        }
    }
}
