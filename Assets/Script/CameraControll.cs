using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

    //Скрипт для передвижения камеры за персонажем

    public GameObject Player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Player.transform.position + offset;
	}
}
