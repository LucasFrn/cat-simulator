using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pao : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode teclaCorreta;
    public SpriteRenderer minhaSprite;
    public Sprite upArrow,downArrow,leftArrow,rightArrow;
    public static float speed = 3f;
    void Start()
    {
        
        int tecla=Random.Range(1,5);
        switch(tecla){
            case 1: teclaCorreta=KeyCode.UpArrow; minhaSprite.sprite=upArrow; break;
            case 2: teclaCorreta=KeyCode.DownArrow; minhaSprite.sprite=downArrow; break;
            case 3: teclaCorreta=KeyCode.RightArrow; minhaSprite.sprite=rightArrow; break;
            case 4: teclaCorreta=KeyCode.LeftArrow; minhaSprite.sprite=leftArrow; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*speed*Time.deltaTime,Space.World);
        if(Input.GetKeyDown(KeyCode.Escape)){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider colidido){
        if(colidido.CompareTag("FinalEsteira")){
            ControllerMiniGamePao.controllerMiniGamePao.nPerdidos++;
            Destroy(gameObject);
        }
    }
    void OnDestroy(){
        ControllerMiniGamePao.controllerMiniGamePao.nPaesAtivos--;
    }
    static public void DefineSpeed(int dif){
        switch(dif){
            case 1: speed = 8f;break;
            case 2: speed = 10f;break;
            case 3: speed = 12f;break;
            default: speed = 12f;break;
        }
    }
}
