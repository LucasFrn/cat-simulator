using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEvents {
    public event Action onEnterGarden;
    public void EnterGarden(){
        if(onEnterGarden!=null){
            onEnterGarden();
        }
    }
    public event Action onLeaveGarden;
    public void LeaveGarden(){
        if(onLeaveGarden!=null){
            onLeaveGarden();
        }
    }
    public event Action<int> onPlantaSelecionada;
    public void PlantaSelecionada(int plantaSelecionada){
        if(onPlantaSelecionada!=null){
            onPlantaSelecionada(plantaSelecionada);
        }
    }
    public event Action<int> onPlantaColhida;
    public void PlantaColida(int valor){
        if(onPlantaColhida!=null){
            onPlantaColhida(valor);
        }
    }
    public event Action onTerraArada;
    public void ArarTerra(){
        if(onTerraArada!=null){
            onTerraArada();
        }
    }
    public event Action onPlantaPlantada;
    public void PlantaPlantada(){
        if(onPlantaPlantada!=null){
            onPlantaPlantada();
        }
    }
    public event Action onPlantaRegada;
    public void PlantaRegada(){
        if(onPlantaRegada!=null){
            onPlantaRegada();
        }
    }

    public event Action onFicouDia;
    public void FicouDia(){
        if(onFicouDia!=null){
            onFicouDia();
        }
    }
}
