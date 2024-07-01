using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class LuzManager : MonoBehaviour//,IDataPersistance
{
    [SerializeField] private Light LuzDirecional;
    [SerializeField] private MusicaController musica;
    [SerializeField] private LuzPreset preset;
    public float ratioPassagemDoTempo = 20f;//ratio = 20 resulta em cada hora durando 20 segundos e etc
                                            //diminuir ao tomar banho
    [SerializeField, Range(0, 24)] public float HoraDoDia;
    bool avisoDia;

    void Start(){
        avisoDia=false;
        if(GameManager.Instance!=null){
            if(GameManager.Instance.overrideSaveToGameManager){
                //HoraDoDia=GameManager.Instance.HoraDoDiaAoTrocarCena;
            }
        }
    }
    private void FixedUpdate()
    {
        if (preset == null)
        { return; }
        if (Application.isPlaying)
        {
            HoraDoDia += Time.fixedDeltaTime*Time.timeScale/ratioPassagemDoTempo;
            HoraDoDia %= 24;
            AtualizaLuz(HoraDoDia);
        }
        else
        {
            AtualizaLuz(HoraDoDia);
        }
        if(HoraDoDia>=6&&HoraDoDia<=7&&!avisoDia){
            GameEventsManager.instance.gardenEvents.FicouDia();
            avisoDia=true;
        }
        if(HoraDoDia>=7&&avisoDia){
            avisoDia=false;
        }
        if (HoraDoDia >= 18 || HoraDoDia <= 6) musica.Noite();
        else musica.Dia();
    }
    private void AtualizaLuz(float tempo)
    {
        RenderSettings.ambientLight = preset.CorAmbiente.Evaluate(tempo);
        RenderSettings.fogColor = preset.CorFog.Evaluate(tempo);

        if (LuzDirecional != null)
        {
            LuzDirecional.color = preset.CorDirecional.Evaluate(tempo);
            LuzDirecional.transform.localRotation = Quaternion.Euler(new Vector3((tempo * 15f) - 90f, -170f, 0));
        }
    }

    private void OnValidate()
    {
        if (LuzDirecional = null)
        {
            return;
        }
        if (RenderSettings.sun != null)
        {
            LuzDirecional = RenderSettings.sun;
        }
        else
        {
            Light[] luzes = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in luzes)
            {
                if (light.type == LightType.Directional)
                {
                    LuzDirecional = light;
                }
            }
        }
    }

    /* public void LoadData(GameData data)
    {
        HoraDoDia=data.horaDoDia;
    }

    public void SaveData(GameData data)
    {
        data.horaDoDia=HoraDoDia;
    } */
}
