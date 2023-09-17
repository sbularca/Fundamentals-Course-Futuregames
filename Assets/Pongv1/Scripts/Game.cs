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


    /// <summary>
    /// Returns the object bounds position based on the render. This works for objects where the bounds closely match the object size.
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="position"></param>
    /// <returns></returns>
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

        // players are a list. I know that player 1 is at index 0 and player 2 is at index 1 as a convention.
        // I could have used a dictionary to map the player to the score, but I didn't want to overcomplicate things.
        // Here I am just setting their starting data
        foreach(var player in players) {
            player.Speed = initialPaddleSpeed;
            player.WallBoundaries = wallsBoundaries;
        }

        // setting start score
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

    // I am checking the score based on the ball position vs paddles position. If the ball is behind the paddles + 1m, then the other player scored
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


    // function that makes sure the ball will shoot in a random direction
    private void SetRandomBallRotation() {
        var randomAngle = UnityEngine.Random.Range(-45f, 45f);
        var flip = UnityEngine.Random.Range(0, 2);
        if(flip == 0) {
            randomAngle += 180f;
        }
        ball.transform.rotation = Quaternion.Euler(ball.transform.rotation.eulerAngles.x, ball.transform.rotation.eulerAngles.y, ball.transform.rotation.eulerAngles.z + randomAngle);
    }
}
