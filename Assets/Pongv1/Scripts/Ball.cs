using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float Speed { get; set; }

    private Vector3 startPosition;
    private Quaternion startRotation;
    private readonly float reflectionFactor = 30f;
    private bool collided;

    private void Start() {
        startPosition = transform.position;
        startRotation = transform.rotation;

        Game.resetPaddles += ResetPosition;
    }
    private void ResetPosition() {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("wall")) {
            transform.right = Vector3.Reflect(transform.right, Vector3.up);
            return;
        }

        CalculateBallRotationBasedOnPaddleHit(other);
        transform.right = Vector3.Reflect(transform.right, Vector3.right);
    }

    private void Update() {
        if(Speed == 0f) {
            return;
        }

        transform.position += transform.right * Speed * Time.deltaTime;

        // there is a float point precision issue that makes the ball go out of bounds
        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z);
    }

    private void CalculateBallRotationBasedOnPaddleHit(Collision collision) {
        var collisionPoint = collision.contacts[0].point;
        var distanceFromCenter = Math.Abs(collisionPoint.y - collision.collider.transform.position.y);
        if (collisionPoint.y > collision.collider.transform.position.y) {
            distanceFromCenter *= -1;
        }
        var height = collision.collider.bounds.size.y / 2;
        var angle = Remap(distanceFromCenter, -height, height, -reflectionFactor, reflectionFactor);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - angle);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
