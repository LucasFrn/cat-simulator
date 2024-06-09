using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GardenInfo
{
    Dictionary<Vector3Int,TileInfo> gardenInfo;
    [SerializeField]
    Grid grid;
    [SerializeField]
    BibliotecaChao bibliotecaChao;
    [SerializeField]
    BibliotecaPlantas bibliotecaPlantas;
    
    public GardenInfo(Grid grid, BibliotecaChao bibliotecaChao, BibliotecaPlantas bibliotecaPlantas){
        gardenInfo = new();
        this.grid = grid;
        this.bibliotecaChao = bibliotecaChao;
        this.bibliotecaPlantas = bibliotecaPlantas;
    }

    public void HoeAt(Vector3Int gridPos){
        if(gardenInfo.ContainsKey(gridPos)==false){
            TileInfo novoTile = new TileInfo();
            gardenInfo[gridPos]=novoTile;
            gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(gridPos),Quaternion.identity);
            gardenInfo[gridPos].Hoed=true;
            GameEventsManager.instance.gardenEvents.ArarTerra();
            
        }
        else{
            if(gardenInfo[gridPos].Hoed){//já esta hoed
                if(DebugManager.debugManager.DEBUG){
                    Debug.Log($"A pos {gridPos} já esta hoed");
                }
            }
            else{//não esá hoed
                if(gardenInfo[gridPos].Watered||gardenInfo[gridPos].Planted){//não ta hoed, mas tem agua ou planta
                    if(DebugManager.debugManager.DEBUG){
                        Debug.Log($"A pos {gridPos} já esta aguada ou plantada, então não posso hoe");
                    }
                }
                else{//não está hoed, nem aguada e nem plantada, tecnicamente n deveria existir, mas vai que né
                    gardenInfo[gridPos].Hoed=true;
                    GameObject.Destroy(gardenInfo[gridPos].floorPrefab.gameObject);
                    gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(gridPos),Quaternion.identity);
                    GameEventsManager.instance.gardenEvents.ArarTerra();
                }
            }
        }
    }
    public void PlantAt(Vector3Int gridPos,int plantaID){// ADICIONAR UM CHECK DE N TER PLANTA NO LUGAR
        if(gardenInfo.ContainsKey(gridPos)){
            if(!gardenInfo[gridPos].Planted){
                if(gardenInfo[gridPos].Hoed||gardenInfo[gridPos].Watered){
                    PlantInfo novaPlanta = new PlantInfo(plantaID,bibliotecaPlantas);
                    gardenInfo[gridPos].plantInfo = novaPlanta;
                    gardenInfo[gridPos].Planted=true;
                    gardenInfo[gridPos].plantPrefab = GameObject.Instantiate(bibliotecaPlantas.catalogoPlantas[plantaID].prefabs[0],grid.CellToWorld(gridPos),Quaternion.identity);
                    GameEventsManager.instance.gardenEvents.PlantaPlantada();
                }
                else{
                    if(DebugManager.debugManager.DEBUG){
                        Debug.Log("Impossivel plantar uma planta na grama");
                    }
                }
            }
            else{
                if(DebugManager.debugManager.DEBUG){
                    Debug.Log($"já tem uma planta plantada em {gridPos}");
                }
            }
        }
    }
    public void RegarAt(Vector3Int gridPos){
        if(gardenInfo.ContainsKey(gridPos)){
            if(gardenInfo[gridPos].Hoed==true){
                if(gardenInfo[gridPos].Watered==false){
                    gardenInfo[gridPos].Watered=true;
                    gardenInfo[gridPos].Hoed=false;
                    GameObject.Destroy(gardenInfo[gridPos].floorPrefab.gameObject);
                    gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Watered],
                                                                            grid.CellToWorld(gridPos),Quaternion.identity);
                    GameEventsManager.instance.gardenEvents.PlantaRegada();
                }
                else{
                    if(DebugManager.debugManager.DEBUG){
                        Debug.Log("Já estou regado");
                    }
                }
            }
            if(DebugManager.debugManager.DEBUG){
                Debug.Log("Não estou hoed");
            }
        }
    }
    public void CrescerPlantas(){
        TileInfo meuTile;
        Vector3Int pos;
        List<Vector3Int>keysToRemove = new();
        foreach (var item in gardenInfo)
        {
            pos=item.Key;
            meuTile=item.Value;
            if(meuTile.Planted){//ta plantado?
                if(meuTile.Watered){//conferir se ta regado
                    if(meuTile.plantInfo.FaseAtual<meuTile.plantInfo.FasesParaCrescer){//ainda não está maduro?
                        meuTile.plantInfo.FaseAtual++;
                        GameObject.Destroy(meuTile.plantPrefab.gameObject);
                        meuTile.plantPrefab = GameObject.Instantiate(bibliotecaPlantas.catalogoPlantas[meuTile.plantInfo.ID].prefabs[meuTile.plantInfo.FaseAtual-1],
                                                                    grid.CellToWorld(pos),Quaternion.identity);
                    }
                    else{//planta ta regada mas já está madura
                        if(DebugManager.debugManager.DEBUG){
                            Debug.Log("Estou maduro já, então não cesci");
                        }
                    }
                    //mudar de regado pra hoed
                    meuTile.Watered=false;
                    meuTile.Hoed=true;
                    GameObject.Destroy(meuTile.floorPrefab.gameObject);
                    meuTile.floorPrefab = GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(pos),Quaternion.identity);
                }
                else{//tem planta mas não está regado
                    if (DebugManager.debugManager.DEBUG){
                        Debug.Log("Não estou regado, então não preciso fazer nada;");
                    }
                }
            }
            else{//não tem planta -> regredir 1 estagio
                if(meuTile.Hoed){
                    /*virar grama
                    meuTile.Hoed=false;
                    GameObject.Destroy(meuTile.floorPrefab.gameObject);
                    meuTile.floorPrefab = GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Grass],grid.CellToWorld(pos),Quaternion.identity);
                    */
                    //tirar do dicionario
                    GameObject.Destroy(meuTile.floorPrefab.gameObject);
                    keysToRemove.Add(pos);
                }
                if(meuTile.Watered){//esse teste precisa ser feito depois do teste do hoed, pq se ele for feito antes vai cair nos 2, já que ele muda o tile pra hoed
                    meuTile.Watered=false;
                    meuTile.Hoed=true;
                    GameObject.Destroy(meuTile.floorPrefab.gameObject);
                    meuTile.floorPrefab = GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(pos),Quaternion.identity);
                }
            } 
        }
        foreach(Vector3Int item in keysToRemove){
            gardenInfo.Remove(item);
        }
    }
    public void ColherAt(Vector3Int gridPos){ // Adicionar Petiscos ao colher 
        if(gardenInfo.ContainsKey(gridPos))
        {   
            if(gardenInfo[gridPos].Planted){//ta plantado
                if(gardenInfo[gridPos].plantInfo.FaseAtual==gardenInfo[gridPos].plantInfo.FasesParaCrescer){
                    int valorRecebido = gardenInfo[gridPos].plantInfo.Valor;
                    gardenInfo[gridPos].Hoed=false;
                    gardenInfo[gridPos].Watered=false;
                    gardenInfo[gridPos].Planted=false;
                    gardenInfo[gridPos].plantInfo=null;
                    GameObject.Destroy(gardenInfo[gridPos].plantPrefab.gameObject);
                    GameObject.Destroy(gardenInfo[gridPos].floorPrefab.gameObject);
                    /*Colocar grama
                    gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Grass],
                                                                            grid.CellToWorld(gridPos),Quaternion.identity);*/
                    //remover do dicionario
                    gardenInfo.Remove(gridPos);
                    GameEventsManager.instance.gardenEvents.PlantaColida(valorRecebido);
                }
                else{
                    if(DebugManager.debugManager.DEBUG){
                        Debug.Log("A planta não está madura, impossivel colher");
                    }
                }
            }
            else{
            if(DebugManager.debugManager.DEBUG){
                Debug.Log("Não tem planta aqui");
            }
        }
        }
        else{
            if(DebugManager.debugManager.DEBUG){
                Debug.Log("Não tem nada aqui");
            }
        }
    }

    public void LoadData(GardenData[] loadedGarden)
    {
        gardenInfo.Clear();
        for(int i=0;i<loadedGarden.Length;i++){
            AdicionarTileLoadado(loadedGarden[i]);
        }
    }
    private void AdicionarTileLoadado(GardenData itemLoadado){
        TileInfo novoTile = new TileInfo(itemLoadado);
        if(novoTile.Planted)
        {   
            novoTile.plantInfo=new PlantInfo(itemLoadado.plantID,bibliotecaPlantas);
            novoTile.plantInfo.FaseAtual=itemLoadado.plantStage;
        }
        gardenInfo[itemLoadado.pos]=novoTile;
        AjustesDoLoad(novoTile,itemLoadado.pos);
    }
    private void AjustesDoLoad(TileInfo tileInfoAjuste,Vector3Int pos){
        if(tileInfoAjuste.Planted){//está plantado
            tileInfoAjuste.plantPrefab=GameObject.Instantiate(bibliotecaPlantas.catalogoPlantas[tileInfoAjuste.plantInfo.ID].prefabs[tileInfoAjuste.plantInfo.FaseAtual-1],
                                                                grid.CellToWorld(pos),Quaternion.identity);
            //ajuste chao
            if(tileInfoAjuste.Watered){//está plantado e regado
                tileInfoAjuste.floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Watered],grid.CellToWorld(pos),Quaternion.identity);
            }
            else{//está plantado e não está regado
                tileInfoAjuste.floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(pos),Quaternion.identity);
            }
        }
        else{//sem planta
            if(tileInfoAjuste.Watered){//sem planta e regado
                tileInfoAjuste.floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Watered],grid.CellToWorld(pos),Quaternion.identity);
            }
            else{//sem planta e sem agua
                if(tileInfoAjuste.Hoed){//sem planta, sem agua, e hoed
                    tileInfoAjuste.floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Hoed],grid.CellToWorld(pos),Quaternion.identity);
                }
                else{//sem plata,sem agua e sem hoed (obs: isso pode estar assim no dicionario quando se colhe uma planta, questionavel? sim, mas eu fiz assim ¯\_(ツ)_/¯)
                    tileInfoAjuste.floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[(int)BibliotecaChao.TiposChao.Grass],grid.CellToWorld(pos),Quaternion.identity);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        GardenData[] newGardenDatas = new GardenData[gardenInfo.Count];
        int aux = 0;
        foreach(var item in gardenInfo){
            newGardenDatas[aux]= new AdapterGardenData(item.Value);
            newGardenDatas[aux].pos = item.Key;
            //Debug.Log($"Pos: {newGardenDatas[aux].pos}\nHoed: {newGardenDatas[aux].hoed}\nWatered: {newGardenDatas[aux].watered}\nPlanted: {newGardenDatas[aux].planted}"+
            //    $"\nPlantId: {newGardenDatas[aux].plantID}\nEstagio: {newGardenDatas[aux].plantStage}");
            aux++;
        }
        data.gardenDatas=newGardenDatas;
    }
}

public class TileInfo{
    public bool Hoed{get; set;}
    public bool Planted {get; set;}
    public bool Watered {get; set;}
    public PlantInfo plantInfo {get; set;}
    public GameObject floorPrefab;
    public GameObject plantPrefab;
    public TileInfo(){
        Hoed=false;
        Planted = false;
        Watered= false;
        plantInfo = null;
        floorPrefab = null;
        plantPrefab = null;
    }
    public TileInfo(GardenData itemLoadado){
        this.Hoed=itemLoadado.hoed;
        this.Watered=itemLoadado.watered;
        this.Planted=itemLoadado.planted;
        this.plantInfo=null;//não tenho acesso a biblioteca ainda
        this.floorPrefab=null;//não tenho acesso a biblioteca ainda
        this.plantPrefab=null;//não tenho acesso a biblioteca ainda
    }
}

public class PlantInfo{
    [field: SerializeField]
    public string Nome {get; private set;}
    [field: SerializeField]
    public int ID {get; private set;}
    [field: SerializeField]
    public int Valor {get; private set;}
    [field: SerializeField]
    public int FaseAtual {get; set;} // vai de 1->FasesParaCrescer, para acessar o array lembrar de tirar -1
    [field: SerializeField]
    public int FasesParaCrescer {get; private set;}
    public PlantInfo(int ID, BibliotecaPlantas bibliotecaPlantas){
        Nome=bibliotecaPlantas.catalogoPlantas[ID].Nome;
        this.ID = ID;
        this.Valor = bibliotecaPlantas.catalogoPlantas[ID].Valor;
        FaseAtual = 1;
        FasesParaCrescer = bibliotecaPlantas.catalogoPlantas[ID].NFases;
    }
}
/* public class Semente{
    public int quantidadeSementes;
    public int plantaID;

    public Semente(int quantidadeSementes,int plantaID){
        this.quantidadeSementes=quantidadeSementes;
        this.plantaID=plantaID;
    }
} */
