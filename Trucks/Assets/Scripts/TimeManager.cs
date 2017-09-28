
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownLength = 2.0f;
    public float abilityDuration = 4.0f;
    private bool isSlowMotion = false;

    void Update()
    {

        if (isSlowMotion)
        {
            abilityDuration -= Time.unscaledDeltaTime;
        }

        if (abilityDuration < 0f && isSlowMotion)
        {
           
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            if (Time.timeScale == 1f)
            {
                isSlowMotion = false;
            }
        }
    }
    public void bulletTime()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        abilityDuration = 4.0f;
        isSlowMotion = true;
    }
}
