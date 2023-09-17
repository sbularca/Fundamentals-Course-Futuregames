using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour {
    [Header("Paddle Control")]
    [SerializeField] private InputAction movementAction;

    public float Speed { get; set; }
    public Vector2 WallBoundaries { get; set; }

    private Vector3 startPosition;
    private Renderer renderer;


    private void Start() {
        movementAction.Enable();
        Game.resetPaddles += ResetPosition;
        startPosition = transform.position;
        renderer = transform.GetComponent<Renderer>();
    }

    private void ResetPosition() {
        transform.localPosition = startPosition;
    }

    private void Update() {
        if (movementAction.IsPressed()) {
            var movementValue = movementAction.ReadValue<float>();
            var newPosition =  transform.position + new Vector3(0, Speed * movementValue * Time.deltaTime, 0);
            var paddleBoundsPosition = Game.GetBoundsPosition(renderer, newPosition);
            if (paddleBoundsPosition.top >= WallBoundaries.x || paddleBoundsPosition.bottom <= WallBoundaries.y) {
                return;
            }
            transform.position = newPosition;
        }
    }

}
