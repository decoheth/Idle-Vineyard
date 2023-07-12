using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaskTimer : MonoBehaviour
{

    public int CaskId;
    public float caskTime;
    float currentTime;

    void Start()
    {
        currentTime = caskTime;
        InvokeRepeating("Progress", 1.0f, 1.0f);
    }

    void Progress()
    {
        // Decrease currentTime by 1 every second
        currentTime -= 1 * Time.deltaTime;

        float progress = Mathf.Clamp01(currentTime / caskTime);

        //progressBar.value = 1 - progress;
    }
}
