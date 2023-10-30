using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LuzManager : MonoBehaviour
{
    [SerializeField] private Light LuzDirecional;
    [SerializeField] private LuzPreset preset;
    [SerializeField, Range(0, 24)] public float HoraDoDia;


    private void FixedUpdate()
    {
        if (preset == null)
        { return; }
        if (Application.isPlaying)
        {
            HoraDoDia += Time.fixedDeltaTime/20;
            HoraDoDia %= 24;
            AtualizaLuz(HoraDoDia);
        }
        else
        {
            AtualizaLuz(HoraDoDia);
        }
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
}