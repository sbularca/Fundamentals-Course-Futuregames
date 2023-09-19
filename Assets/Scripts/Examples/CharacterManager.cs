using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public CharacterController characterController;

    private Vector3 moveSpeed;

    private void Update() {
        moveSpeed= 3 * new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveSpeed);
    }

    public void LinqAndLambda() {
        List<string> aList = new List<string>(5);
    }
}
