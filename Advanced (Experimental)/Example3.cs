using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example3 : MonoBehaviour {

	[System.Serializable]
	public struct SerializableExampleStruct {
		public float movementDistance;
		public GameObject target;
		public Transform[] positions;
	}

	public SerializableExampleStruct mob;

}
