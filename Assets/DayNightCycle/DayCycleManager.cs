using System.Collections;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;

    private bool isTransitioning = false; // ���� ��������
    private float targetTimeOfDay; // ������� ����� �����
    public int nextScoreThreshold = 10; // ��������� ����� �����
    void Awake()
    {
        // ������������� ������ ������ ��� �� Start()
        TimeOfDay = 0.25f;
    }
    void Start()
    {
        sunIntensity = Sun.intensity;
        UpdateLighting();
    }

    void Update()
    {
        if (!isTransitioning && GameManager.Instance.Score >= GameManager.Instance.nextScoreThreshold)
        {
            GameManager.Instance.nextScoreThreshold += 100; // ����� �����
            ChangeDayNight();
        }

        if (isTransitioning)
        {
            UpdateLighting();
        }
    }

    private void ChangeDayNight()
    {
        float newTime = (TimeOfDay < 0.5f) ? 0.75f : 0.25f; // ���� / ����
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
        float duration = 2f; // ������������ ��������

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
