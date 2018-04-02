using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviorWay : BaseObject {

	// Use this for initialization
	void Start () {
        SetupUtils.PlaceRandomCubes1(m_ObjectCount, m_ObjectPlacementRadius);
    }
	
}
