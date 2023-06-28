using Godot;
using System;
using System.Security.AccessControl;

public partial class Jogador : CharacterBody2D
{
	private Vector2 velocity;
	private Vector2 direction;
	public Vector2 savePoint;
	public const float Speed = 280.0f;
	public const float JumpVelocity = -350.0f;
	private float minHeigth = 310f;
	private bool lookingRight = true;
	private bool doubleJump;
	public bool finalPosition;
	public bool isOnSavePoint;
	private bool allDiamondsColected;
	public int diamante;
	private int numberOfDiamonds;
	public int lives;
	public string nextMap;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D animate;
	private AudioStreamPlayer somDePulo;

	public override void _Ready() {
		animate = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		allDiamondsColected = false;
		diamante = 0;
		lives = 3;
		somDePulo = GetNode<AudioStreamPlayer>("Som/Pulo");
	} //fim do ready

	public override void _PhysicsProcess(double delta)
	{
		//inercia inicial
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Moves(); //function to the input of the movements

		ContarDiamantes(); // count the diamonds that were collected
		
		OutOfTheMap(); //check the position of the player to see if is out of the map

		GameOver(); // check if the game is Over

		EndOfMap(); // check if the map is concluded

		Velocity = velocity;
		MoveAndSlide();

		Animation(velocity); // Animation of the player

		CountLivesAnimation();

	} //fim do process

	private void Moves()
	{
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			doubleJump = true;
			somDePulo.Play();
		}
		else if (Input.IsActionJustPressed("ui_accept") && !IsOnFloor() && doubleJump)
		{
			velocity.Y = JumpVelocity;
			doubleJump = false;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero && IsOnFloor())
		{
			velocity.X = direction.X * Speed;
		}
		else if (IsOnFloor())
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		else if (direction != Vector2.Zero && !IsOnFloor())
		{
			if(velocity.X > 0 && direction.X < 0)
			{
				velocity.X = direction.X * (Speed / 5);
			} 
			else if (velocity.X < 0 && direction.X > 0)
			{
				velocity.X = direction.X * (Speed / 5);
			}
			else velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed/2, Speed);
		}
	}

	private void Animation(Vector2 velocity) {
		if (!IsOnFloor()){
			animate.Play("jump");
		} else {
			if (velocity.X != 0) {
				animate.Play("run");
			} else {
				animate.Play("idle");
			}
		}

		if (velocity.X > 0 && lookingRight)
		{
			lookingRight = false;
			animate.FlipH = true;
			//Scale *= new Vector2(-1, 1);
		}
		else if (velocity.X < 0 && !lookingRight)
		{
			lookingRight = true;
			animate.FlipH = false;
			//Scale *= new Vector2(-1, 1);
		}
	}

	private void CountLivesAnimation()
	{
		if(lives == 3){
			//show all 3
			GetTree().CallGroup("HUD", "ShowVida1");
			GetTree().CallGroup("HUD", "ShowVida2");
			GetTree().CallGroup("HUD", "ShowVida3");
		} else if(lives == 2){
			//show 1 and 2
			GetTree().CallGroup("HUD", "ShowVida1");
			GetTree().CallGroup("HUD", "ShowVida2");
			GetTree().CallGroup("HUD", "HideVida3");
		} else {
			//show 1
			GetTree().CallGroup("HUD", "ShowVida1");
			GetTree().CallGroup("HUD", "HideVida2");
			GetTree().CallGroup("HUD", "HideVida3");
		}
		GetTree().CallGroup("HUD", "CountDiamonds", diamante);
	}
	
	public void SetStart(Vector2 startPosition)
	{
		GlobalPosition = startPosition;
		savePoint = startPosition;
	}

	public void SetNumberOfDiamonds(int setNumberOfDiamonds)
	{
		numberOfDiamonds = setNumberOfDiamonds;
	}

	public void ContarDiamantes()
	{
		if (diamante == numberOfDiamonds && allDiamondsColected == false)
		{
			GD.Print("Todos os diamantes foram coletados");
			allDiamondsColected = true;
		}
	}

	public void ReturnToSavePoint()
	{
		GlobalPosition = savePoint;
	}
	
	public void SaveNewSavePoint()
	{
		savePoint = GlobalPosition;
	}

	private void OutOfTheMap()
	{
		if(GlobalPosition.Y > minHeigth)
		{
			ReturnToSavePoint();
			lives--;
		}
	}

	public void SetNextMap(string setNextMap)
	{
		nextMap = setNextMap;
	}

	private void EndOfMap()
	{
		if(finalPosition && allDiamondsColected)
		{
			GetTree().ChangeSceneToFile(nextMap);
			diamante = 0;
		}
	}

	private void GameOver()
	{
		if (lives == 0)
		{
			GD.Print("Game Over");
			GetTree().ChangeSceneToFile("game_over.tscn");
		}
	}

} //fim da classe
