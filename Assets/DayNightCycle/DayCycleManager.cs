using System.Collections;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 30f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    private bool isTransitioning = false; // Флаг перехода
    private float targetTimeOfDay; // Целевое время суток
    private int nextScoreThreshold = 10; // Следующий порог очков

    void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;
    }

    void Update()
    {
        if (!isTransitioning && GameManager.Instance.Score >= nextScoreThreshold)
        {
            nextScoreThreshold += 10; // порог смены
            ChangeDayNight();
        }

        if (isTransitioning)
        {
            UpdateLighting();
        }
    }

    private void ChangeDayNight()
    {
        float newTime = (TimeOfDay < 0.5f) ? 0.75f : 0.25f; // день / ночь
        TriggerTimeChange(newTime);
    }

    public void TriggerTimeChange(float targetTime)
    {
        if (isTransitioning) return;

        targetTimeOfDay = targetTime;
        StartCoroutine(FastTransition(targetTimeOfDay));
    }

    private IEnumerator FastTransition(float targetTime)
    {
        isTransitioning = true;
        float startTime = TimeOfDay;
        float elapsedTime = 0f;
        float duration = 2f; // длительность перехода

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            TimeOfDay = Mathf.Lerp(startTime, targetTime, elapsedTime / duration);
            UpdateLighting();
            yield return null;
        }

        TimeOfDay = targetTime;
        UpdateLighting();
        isTransitioning = false;
    }

    private void UpdateLighting()
    {
        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyCurve.Evaluate(TimeOfDay));
        DynamicGI.UpdateEnvironment();

        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 0, 180);
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);

        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f + 180, 0, 180);
        Moon.intensity = 0.7f * MoonCurve.Evaluate(TimeOfDay);
    }
}
