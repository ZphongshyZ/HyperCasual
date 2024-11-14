using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    [SerializeField] private int roadWidth;
    [SerializeField] private float slideSpeed;

    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;

    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallback;
    }

    private void Update()
    {
        if(this.canMove)
        {
            this.MoveFroward();
            this.TurnByMouse();
        }
    }

    private void StartMoving()
    {
        this.canMove = true;
        this.playerAnimator.Run();
    }

    private void StopMoving()
    {
        this.canMove = false;
        this.playerAnimator.Idle();
    }

    private void GameStateChangeCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
            this.StartMoving();
        else if (gameState == GameManager.GameState.GameOver)
            this.StopMoving();
        else if (gameState == GameManager.GameState.LevelComplete)
            this.StopMoving();
    }    

    private void MoveFroward()
    {
        this.transform.position += Vector3.forward * this.speed * Time.deltaTime;
    }

    private void TurnByMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.clickScreenPosition = Input.mousePosition;
            clickPlayerPosition = this.transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - this.clickPlayerPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= this.slideSpeed;

            Vector3 position = this.transform.position;
            position.x = this.clickPlayerPosition.x +xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());
            this.transform.position = position;
        }
    }
}
