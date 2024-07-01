using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour, IDataPersistance
{
    public float timeMultiplier;
    //[SerializeField]float startHour;
    [SerializeField]TextMeshProUGUI timeText;
    [SerializeField]Light sunLight;
    [SerializeField]float sunRiseHour;
    [SerializeField]float sunSetHour;
    [SerializeField]Color dayAmbientLight;
    [SerializeField]Color nightAmbientLight;
    [SerializeField]AnimationCurve lightChanceCurve;
    [SerializeField]float maxSunLightIntensity;
    [SerializeField]Light moonLight;
    [SerializeField]float maxMoonLightIntensity;
    [SerializeField]GameObject[] lights;
    [SerializeField]ParticleSystem fogueira;
    [SerializeField]ParticleSystem vagalume;
    [SerializeField]MusicaController musicaController;
    bool coisasLigadas;
    TimeSpan sunRiseTime;
    TimeSpan sunSetTime;
    DateTime currentTime;

    bool startOfDayCheck;
    bool startOfNightCheck;

    void Start()
    {
        //currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        currentTime= currentTime.Add(GameManager.Instance.GanhoTempoAoTrocarCena);
        GameManager.Instance.GanhoTempoAoTrocarCena=new TimeSpan(0,0,0);
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
        startOfNightCheck=false;
        startOfDayCheck=false;
        if(currentTime.Hour>19){
            coisasLigadas=false;
        }else{
            coisasLigadas=true;
        }
        ManageBregues();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        if(currentTime.Hour==6&&!startOfDayCheck){
            startOfDayCheck=true;
            startOfNightCheck=false;
            Debug.Log("Ficou dia");
            GameEventsManager.instance.miscEvents.FicouDia(10);
            GameEventsManager.instance.gardenEvents.FicouDia();
            ManageBregues();
            

        }
        if(currentTime.Hour==19&&!startOfNightCheck){
            startOfDayCheck=false;
            startOfNightCheck=true;
            Debug.Log("Ficou noite");
            ManageBregues();
            musicaController.Noite();
        }
    }
    void UpdateTimeOfDay(){
        currentTime = currentTime.AddSeconds(Time.fixedDeltaTime*timeMultiplier);
        if(timeText!=null){
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
    void RotateSun(){
        float sunLightRotation;
        if(currentTime.TimeOfDay>sunRiseTime && currentTime.TimeOfDay<sunSetTime){
            TimeSpan sunriseTosunsetDuration = CalculateTimeDifference(sunRiseTime,sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRiseTime,currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes/sunriseTosunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0,180,(float)percentage);
        }
        else{
            TimeSpan sunsetTosunriseDuration = CalculateTimeDifference(sunSetTime,sunRiseTime);
            TimeSpan timeSinceSunSet = CalculateTimeDifference(sunSetTime,currentTime.TimeOfDay);
            double percentage = timeSinceSunSet.TotalMinutes/sunsetTosunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180,360,(float)percentage);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation,Vector3.right);
    }
    void UpdateLightSettings(){
        float dorProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0,maxSunLightIntensity,lightChanceCurve.Evaluate(dorProduct));
        //moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity,0,lightChanceCurve.Evaluate(dorProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight,dayAmbientLight,lightChanceCurve.Evaluate(dorProduct));
    }
    TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime){
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds<0){
            difference+=TimeSpan.FromHours(24);
        }
        return difference;
    }
    void ManageBregues(){
        if(coisasLigadas){
            foreach(GameObject light in lights){
                light.SetActive(false);
            }
            fogueira.Stop();
            vagalume.Stop();
            coisasLigadas=false;
        }
        else{
            foreach(GameObject light in lights){
                light.SetActive(true);
            }
            fogueira.Play();
            vagalume.Play();
            coisasLigadas=true;
        }
    }
    public void LoadData(GameData data)
    {
        TimeSpan hora = new TimeSpan(data.horaDoDia[0],data.horaDoDia[1],data.horaDoDia[2]);
        currentTime=DateTime.Today.Add(hora);
    }

    public void SaveData(GameData data)
    {
        data.horaDoDia= new int[] {currentTime.Hour,currentTime.Minute,currentTime.Second};
    }
}
