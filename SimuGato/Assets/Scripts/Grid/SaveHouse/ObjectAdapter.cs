using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdapter : ObjectData
{
    public ObjectAdapter(PlacebleObject obj)
    {
        position = obj.transform.position;
        rotation = obj.transform.rotation.eulerAngles;
        objectName = obj.GetComponent<PlacebleObject>()._name;
    }
}
