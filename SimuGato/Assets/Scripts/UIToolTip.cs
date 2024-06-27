using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIToolTip : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] GameObject PainelToolTip;
    // Start is called before the first frame update
    void Start()
    {
        if(PainelToolTip!=null){
            PainelToolTip.SetActive(false);
        }
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        PainelToolTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PainelToolTip.SetActive(false);
    }
}
