using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour,IObserver
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
    // Start is called before the first frame update
    void Start()
    {
        gardenInfo = new(grid,bibliotecaChao,bibliotecaPlantas);
        SubjectPlayer.instance.AddObserver(this);
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
            gardenInfo.PlantAt(lastCellPos,1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            gardenInfo.RegarAt(lastCellPos);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            //gardenInfo.CrescerPlantas();
            SubjectPlayer.instance.NotifyObserver();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            gardenInfo.ColherAt(lastCellPos);
        }
    }
     public void NotifyObserver(){
        gardenInfo.CrescerPlantas();
    }
    
}
