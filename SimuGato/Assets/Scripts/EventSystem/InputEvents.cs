using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents{
    public event Action onSubmitPressed;
    public void SubmitPressed(){
        if(onSubmitPressed!=null){
            onSubmitPressed();
        }
    }
}