using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour, IDataPersistance
{
    [SerializeField]Transform cuboGuia;
    [SerializeField]Grid grid;
    Vector3Int cellNumber,lastCellPos;
    [SerializeField]GameObject contorno;
    [SerializeField] GardenInfo gardenInfo;
    [SerializeField] BibliotecaChao bibliotecaChao;
    [SerializeField] BibliotecaPlantas bibliotecaPlantas;
    bool canInteract;
    enum TiposPlantas{
        Abobora,
        Cenoura,
        Tomate
    }
    [SerializeField]TiposPlantas plantaSelecionada;
    int[] inventarioSementes = new int [Enum.GetNames(typeof(TiposPlantas)).Length];

    public void LoadData(GameData data)
    {
        if(data.quantidadeSementes!=null){
            if(data.quantidadeSementes.Length!=0){
                for(int i =0;i<inventarioSementes.Length;i++){
                    inventarioSementes[i]=data.quantidadeSementes[i];
                }
            }
        }
        if(data.gardenDatas!=null){
            //Debug.Log($"garden datas tem tamanho {data.gardenDatas.Length}");
            if(data.gardenDatas.Length!=0)
                gardenInfo.LoadData(data.gardenDatas);
        }
    }

    public void SaveData(GameData data)
    {
        //Debug.Log("Grid controller vai tentar salvar");
        gardenInfo.SaveData(data);
        data.quantidadeSementes=inventarioSementes;
    }

    void Awake(){
        gardenInfo = new(grid,bibliotecaChao,bibliotecaPlantas);
    }
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden+=EnteredGarden;
        GameEventsManager.instance.gardenEvents.onLeaveGarden+=LeftGarden;
        GameEventsManager.instance.gardenEvents.onPlantaSelecionada+=SelecionaPlanta;
        GameEventsManager.instance.rewardEvents.onSementeRewardRecived+=GanharSementes;
        GameEventsManager.instance.gardenEvents.onFicouDia+=CrescerPlantas;
        
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden-=EnteredGarden;
        GameEventsManager.instance.gardenEvents.onLeaveGarden-=LeftGarden;
        GameEventsManager.instance.gardenEvents.onPlantaSelecionada-=SelecionaPlanta;
        GameEventsManager.instance.rewardEvents.onSementeRewardRecived-=GanharSementes;
        GameEventsManager.instance.gardenEvents.onFicouDia-=CrescerPlantas;
    }
    void Start()
    {
        canInteract=false;
        plantaSelecionada=TiposPlantas.Abobora;
        AtualizarSementes();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract){
            cellNumber=grid.WorldToCell(cuboGuia.position);
            if(cellNumber!=lastCellPos){
                contorno.transform.position=grid.CellToWorld(cellNumber);
                lastCellPos= cellNumber;
            }
            if(DebugManager.debugManager.DEBUG){
                if(Input.GetKeyDown(KeyCode.B)){
                    Debug.Log($"Estamos na celula {cellNumber} e na cord {cuboGuia.position}");
                    Debug.Log($"Estamos na celula {cellNumber} que resulta na local cord {grid.CellToLocal(cellNumber)}");
                    Debug.Log($"Estamos na celula {cellNumber} que resulta na world cord {grid.CellToWorld(cellNumber)}");
                }  
            }
            if(canInteract&&GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
                if(Input.GetKeyDown(KeyCode.Alpha1)){
                    gardenInfo.HoeAt(lastCellPos);
                }
                if(Input.GetKeyDown(KeyCode.Alpha2)){
                    if(inventarioSementes[(int)plantaSelecionada]>0){
                        if(gardenInfo.PlantAt(lastCellPos,(int)plantaSelecionada)){
                            inventarioSementes[(int)plantaSelecionada]--;
                            GameEventsManager.instance.uiEvents.AtualizarQuantidadeSementes((int)plantaSelecionada,inventarioSementes[(int)plantaSelecionada]);
                        }
                    }
                }
                if(Input.GetKeyDown(KeyCode.Alpha3)){
                    gardenInfo.RegarAt(lastCellPos);
                }
                if(Input.GetKeyDown(KeyCode.Alpha4)){
                    gardenInfo.ColherAt(lastCellPos);
                }
                if(Input.GetKeyDown(KeyCode.Alpha0)){
                    CrescerPlantas();
                }
                if(Input.GetKeyDown(KeyCode.R)){
                    SelecionaPlanta(0);
                }
                if(Input.GetKeyDown(KeyCode.T)){
                    SelecionaPlanta(1);
                }
                if(Input.GetKeyDown(KeyCode.Y)){
                    SelecionaPlanta(2);
                }
            }
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            if(Input.GetKeyDown(KeyCode.Alpha8)){
                    GanharSementes(0,5);
                    GanharSementes(1,5);
                    GanharSementes(2,5);
                }
        }
    }
    //Coisas Do Event System
    void CrescerPlantas(){
        gardenInfo.CrescerPlantas();
    }
    void EnteredGarden(){
        canInteract=true;
        contorno.SetActive(true);
    }
    void LeftGarden(){
        canInteract=false;
        contorno.SetActive(false);
    }
    void SelecionaPlanta(int tipo){
        plantaSelecionada = (TiposPlantas)tipo;
    }
    public void GanharSementes(int tipoSemente,int quantidade){
        inventarioSementes[tipoSemente]+=quantidade;
        GameEventsManager.instance.uiEvents.AtualizarQuantidadeSementes(tipoSemente,inventarioSementes[tipoSemente]);
    }
    public void AtualizarSementes(){
        for(int i =0; i<inventarioSementes.Length;i++){
            GameEventsManager.instance.uiEvents.AtualizarQuantidadeSementes(i,inventarioSementes[i]);
        }
    }
}
