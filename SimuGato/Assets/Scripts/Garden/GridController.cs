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
    int[] inventarioSementes = new int [TiposPlantas.GetNames(typeof(TiposPlantas)).Length];

    public void LoadData(GameData data)
    {
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
    }

    void Awake(){
        gardenInfo = new(grid,bibliotecaChao,bibliotecaPlantas);
    }
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden+=EnteredGarden;
        GameEventsManager.instance.gardenEvents.onLeaveGarden+=LeftGarden;
        GameEventsManager.instance.gardenEvents.onPlantaSelecionada+=SelecionaPlanta;
        
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden-=EnteredGarden;
        GameEventsManager.instance.gardenEvents.onLeaveGarden-=LeftGarden;
        GameEventsManager.instance.gardenEvents.onPlantaSelecionada-=SelecionaPlanta;
    }
    void Start()
    {
        canInteract=false;
        plantaSelecionada=TiposPlantas.Abobora;
    }

    // Update is called once per frame
    void Update()
    {
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
                    gardenInfo.PlantAt(lastCellPos,(int)plantaSelecionada);
                    inventarioSementes[(int)plantaSelecionada]--;
                    GameEventsManager.instance.uiEvents.AtualizarQuantidadeSementes((int)plantaSelecionada,inventarioSementes[(int)plantaSelecionada]);
                }
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                gardenInfo.RegarAt(lastCellPos);
            }
            }
            if(Input.GetKeyDown(KeyCode.Alpha4)){
                gardenInfo.ColherAt(lastCellPos);
            }
            if(Input.GetKeyDown(KeyCode.Alpha0)){
                gardenInfo.CrescerPlantas();
        }
    }
    //Coisas Do Event System
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
}
