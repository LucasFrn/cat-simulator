using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsideMiniMap : MonoBehaviour{

	public Transform MinimapCam;
	public float MinimapSize;
	Vector3 TempV3;
    void Start(){
		if(GameManager.Instance.cameraMiniMapa!=null)
        	MinimapCam=GameManager.Instance.cameraMiniMapa.transform;
        MinimapSize=GameManager.Instance.minimapaTamanho;
    }

	void Update () {
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;
	}

	void LateUpdate () {
		// Center of Minimap
		if(MinimapCam==null){
			MinimapCam=GameManager.Instance.cameraMiniMapa.transform;
		}
		else{
			Vector3 centerPosition = MinimapCam.localPosition;

			// Just to keep a distance between Minimap camera and this Object (So that camera don't clip it out)
			centerPosition.y -= 0.5f;

			// Distance from the gameObject to Minimap
			float Distance = Vector3.Distance(transform.position, centerPosition);

			// If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
			// But if the Distance is greater than the MinimapSize, then do this
			if (Distance > MinimapSize)
			{
        	    transform.localScale=new Vector3(.5f,.5f,0);
				// Gameobject - Minimap
				Vector3 fromOriginToObject = transform.position - centerPosition;

				// Multiply by MinimapSize and Divide by Distance
				fromOriginToObject *= MinimapSize / Distance;

				// Minimap + above calculation
				transform.position = centerPosition + fromOriginToObject;
			}
        	else{
        	    transform.localScale=new Vector3(1,1,0);
        	}
		}
	}
}
