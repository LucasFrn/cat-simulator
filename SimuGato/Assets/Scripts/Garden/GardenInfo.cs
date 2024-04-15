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
            gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[1],grid.CellToWorld(gridPos),Quaternion.identity);
            gardenInfo[gridPos].Hoed=true;
            
        }
        else{
            if(gardenInfo[gridPos].Hoed==true||gardenInfo[gridPos].Planted==true){
                if(DebugManager.debugManager.DEBUG){
                    Debug.Log($"A pos {gridPos} já esta hoed ou plantada");
                }
            }
            else{
                gardenInfo[gridPos].Hoed=true;
                gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[1],grid.CellToWorld(gridPos),Quaternion.identity);
            }
        }
    }
    public void PlantAt(Vector3Int gridPos,int plantaID){ //maluco da pra plantar por cima de outra planta
        if(gardenInfo.ContainsKey(gridPos)){
            if(gardenInfo[gridPos].Hoed==true){
                if(gardenInfo[gridPos].Planted==false){
                    PlantInfo novaPlanta = new PlantInfo(plantaID,bibliotecaPlantas);
                    gardenInfo[gridPos].plantInfo = novaPlanta;
                    gardenInfo[gridPos].Planted=true;
                    gardenInfo[gridPos].plantPrefab = GameObject.Instantiate(bibliotecaPlantas.catalogoPlantas[plantaID].prefabs[0],grid.CellToWorld(gridPos),Quaternion.identity);
                }
                else{
                    //adicionar feedback de falha pois já tem uma planta plantada
                    if (DebugManager.debugManager.DEBUG){
                        Debug.Log($"já tem uma planta plantada em {gridPos}");
                    }
                }
            }
            else{
                //adicionar feedback de falha pois não está hoed
                if (DebugManager.debugManager.DEBUG){
                    Debug.Log($"não dapra plantar pq a pos {gridPos} não está hoed");
                }
            }
        }
    }
    public void RegarAt(Vector3Int gridPos){
        if(gardenInfo.ContainsKey(gridPos)){
            if(gardenInfo[gridPos].Hoed==true){
                if(gardenInfo[gridPos].Watered==false){
                    gardenInfo[gridPos].Watered=true;
                    GameObject.Destroy(gardenInfo[gridPos].floorPrefab.gameObject);
                    gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[2],grid.CellToWorld(gridPos),Quaternion.identity);
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
        foreach (var item in gardenInfo)
        {
            pos=item.Key;
            meuTile=item.Value;
            if(meuTile.Watered == true){//adicionar logica que confere se tem uma planta né
                if(meuTile.plantInfo.FaseAtual<meuTile.plantInfo.FasesParaCrescer){
                    meuTile.plantInfo.FaseAtual++;
                    GameObject.Destroy(meuTile.plantPrefab.gameObject);
                    meuTile.plantPrefab = GameObject.Instantiate(bibliotecaPlantas.catalogoPlantas[meuTile.plantInfo.ID].prefabs[meuTile.plantInfo.FaseAtual-1],
                                                                grid.CellToWorld(pos),Quaternion.identity);
                    meuTile.Watered=false;
                    GameObject.Destroy(meuTile.floorPrefab.gameObject);
                    meuTile.floorPrefab = GameObject.Instantiate(bibliotecaChao.catalogoChao[1],grid.CellToWorld(pos),Quaternion.identity);
                    //trocar prefab chao
                }
                else{
                    if (DebugManager.debugManager.DEBUG){
                        Debug.Log("Estou maduro já, então não cesci");
                    }
                }
            }
            else{
                if (DebugManager.debugManager.DEBUG){
                    Debug.Log("Não estou regado, então não cesci");
                }
            }    
        }
    }
    public void ColherAt(Vector3Int gridPos){ // RETORNAR A PLANTA AO INVESNTARIO QUANDO O INVENTARIO FOR CRIADO
        if(gardenInfo.ContainsKey(gridPos))
        {   
            if(gardenInfo[gridPos].plantInfo.FaseAtual==gardenInfo[gridPos].plantInfo.FasesParaCrescer){
                gardenInfo[gridPos].Hoed=false;
                gardenInfo[gridPos].Watered=false;
                gardenInfo[gridPos].Planted=false;
                gardenInfo[gridPos].plantInfo=null;
                GameObject.Destroy(gardenInfo[gridPos].plantPrefab.gameObject);
                GameObject.Destroy(gardenInfo[gridPos].floorPrefab.gameObject);
                gardenInfo[gridPos].floorPrefab=GameObject.Instantiate(bibliotecaChao.catalogoChao[0],grid.CellToWorld(gridPos),Quaternion.identity);
            }
            else
            {
                if(DebugManager.debugManager.DEBUG){
                    Debug.Log("A planta não está crescida");
                }
            }
        }
        else{
            if(DebugManager.debugManager.DEBUG){
                Debug.Log("Não tem nada aqui");
            }
        }
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
    //public void AvancarEstagio(BibliotecaPlantas bibliotecaPlantas){
        
    //}
}

public class PlantInfo{
    [field: SerializeField]
    public string Nome {get; private set;}
    [field: SerializeField]
    public int ID {get; private set;}
    [field: SerializeField]
    public int FaseAtual {get; set;} // vai de 1->FasesParaCrescer, para acessar o array lembrar de tirar -1
    [field: SerializeField]
    public int FasesParaCrescer {get; private set;}
    public PlantInfo(int ID, BibliotecaPlantas bibliotecaPlantas){
        Nome=bibliotecaPlantas.catalogoPlantas[ID].Nome;
        this.ID = ID;
        FaseAtual = 1;
        FasesParaCrescer = bibliotecaPlantas.catalogoPlantas[ID].NFases;
    }
}
public class Semente{
    public int quantidadeSementes;
    public int plantaID;

    public Semente(int quantidadeSementes,int plantaID){
        this.quantidadeSementes=quantidadeSementes;
        this.plantaID=plantaID;
    }
}
