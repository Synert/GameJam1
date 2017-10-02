
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownLength = 2.0f;
    public float abilityDuration = 4.0f;
    public float cooldown = 0.0f;
    private bool isSlowMotion = false;

    void Update()
    {

        if (isSlowMotion)
        {
            abilityDuration -= Time.unscaledDeltaTime;
        }

        if (!isSlowMotion)
        {
            cooldown -= Time.unscaledDeltaTime;
            Debug.Log("Cooldown decreasing");
        }

        if (abilityDuration < 0f && isSlowMotion)
        {
           
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            if (Time.timeScale == 1f)
            {
                isSlowMotion = false;
                cooldown = 10.0f;
                Debug.Log("MAX COOLDOWN");
            }
        }
    }
    public void bulletTime()
    {
        if (cooldown <= 0.0f && !isSlowMotion)
        {
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            abilityDuration = 4.0f;
            isSlowMotion = true;
            Debug.Log("SLOWMOTION");
        }
        else
        {
            Debug.Log("ON COOLDOWN");
        }
    }
}
