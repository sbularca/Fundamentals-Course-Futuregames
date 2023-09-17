using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongBounds {
    public float top;
    public float bottom;
    public float left;
    public float right;
}
public class Game : MonoBehaviour {
    [Header("Boundaries")]
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;

    [Header("Active Objects")]
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle [] players;

    [Header("Settings")]
    [SerializeField] private float initialPaddleSpeed;

    [Header("Input keys")]
    [SerializeField] private InputAction resetAction;

    [Header("UI Elements")]
    [SerializeField] TextMeshPro player1ScoreUI;
    [SerializeField] TextMeshPro player2ScoreUI;

    private int player1Score;
    private int player2Score;


    public static Action resetPaddles;


    public static PongBounds GetBoundsPosition(Renderer renderer, Vector3 position) {
        Vector3 size = renderer.bounds.size;
        PongBounds bounds = new () {
            top = position.y + size.y / 2,
            bottom = position.y - size.y / 2,
            left = position.x - size.x / 2,
            right = position.x + size.x / 2
        };
        return bounds;
    }


    private void Start() {
        resetAction.Enable();
        var topWallHeight = GetBoundsPosition(topWall.GetComponent<Renderer>(), topWall.transform.position).bottom;
        var bottomWallHeight = GetBoundsPosition(bottomWall.GetComponent<Renderer>(), bottomWall.transform.position).top;
        var wallsBoundaries = new Vector2(topWallHeight, bottomWallHeight);

        foreach(var player in players) {
            player.Speed = initialPaddleSpeed;
            player.WallBoundaries = wallsBoundaries;
        }

        player1ScoreUI.text = player1Score.ToString();
        player2ScoreUI.text = player2Score.ToString();

        resetPaddles += ResetPaddles;
    }
    private void ResetPaddles() {
        ball.Speed = 0;
    }

    private void Update() {
        if(resetAction.WasPerformedThisFrame()) {
            resetPaddles?.Invoke();
            ball.Speed = 3;
            SetRandomBallRotation();
        }

        CheckScore();
    }

    private void CheckScore() {
        if (ball.transform.position.x < players[0].transform.position.x - 1f) {
            Score(player1ScoreUI, player1Score);
            Debug.Log("Player 1 scored!");
        }

        if (ball.transform.position.x > players[1].transform.position.x + 1f) {
            Score(player2ScoreUI, player2Score);
            Debug.Log("Player 2 scored!");
        }
    }

    private void Score(TextMeshPro scoreRef, int score) {
        resetPaddles?.Invoke();
        score++;
        scoreRef.text = score.ToString();
    }

    private void SetRandomBallRotation() {
        var randomAngle = UnityEngine.Random.Range(-45f, 45f);
        var flip = UnityEngine.Random.Range(0, 2);
        if(flip == 0) {
            randomAngle += 180f;
        }
        ball.transform.rotation = Quaternion.Euler(ball.transform.rotation.eulerAngles.x, ball.transform.rotation.eulerAngles.y, ball.transform.rotation.eulerAngles.z + randomAngle);
    }
}
