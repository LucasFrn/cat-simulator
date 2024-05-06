using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour, IDataPersistance
{
    [SerializeField]
    Transform cuboGuia;
    [SerializeField]
    Grid grid;
    Vector3Int cellNumber,lastCellPos;
    [SerializeField]
    GameObject contorno;
    [SerializeField] GardenInfo gardenInfo;
    [SerializeField] BibliotecaChao bibliotecaChao;
    [SerializeField] BibliotecaPlantas bibliotecaPlantas;

    public void LoadData(GameData data)
    {
        if(data.gardenDatas!=null){
            Debug.Log($"garden datas tem tamanho {data.gardenDatas.Length}");
            if(data.gardenDatas.Length!=0)
                gardenInfo.LoadData(data.gardenDatas);
        }
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Grid controller vai tentar salvar");
        gardenInfo.SaveData(data);
    }

    void Awake(){
        gardenInfo = new(grid,bibliotecaChao,bibliotecaPlantas);
    }
    void Start()
    {
        
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
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            gardenInfo.HoeAt(lastCellPos);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            gardenInfo.PlantAt(lastCellPos,0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            gardenInfo.RegarAt(lastCellPos);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            gardenInfo.CrescerPlantas();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            gardenInfo.ColherAt(lastCellPos);
        }
    }
    
}
