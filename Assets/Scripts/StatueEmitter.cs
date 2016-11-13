using UnityEngine;
using System.Collections;

public class StatueEmitter : MonoBehaviour {
	public GameObject statue;

	private GameObject statueInstance;
	private int currentBase;

	// Use this for initialization
	void Start () {
		statueInstance = (GameObject)Instantiate (statue);
		statueInstance.GetComponent<Statue> ().emitter = gameObject;
		PlaceStatue (Random.Range (0, gameObject.transform.childCount));
	}

	private void PlaceStatue (int baseNum) {
		currentBase = baseNum;
		statueInstance.GetComponent<Statue> ().SetHalo (false);
		statueInstance.gameObject.transform.position =
			gameObject.transform.GetChild (baseNum).transform.position +
			new Vector3 (0, statueInstance.GetComponent<PolygonCollider2D> ().bounds.extents.y);
	}

	public void StatueSeen () {
		int baseNum = Random.Range (0, gameObject.transform.childCount);
		if (baseNum == currentBase) {
			if (++baseNum >= gameObject.transform.childCount) {
				baseNum = 0;
			}
		}
		PlaceStatue (baseNum);
	}
}
