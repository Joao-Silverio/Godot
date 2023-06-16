using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private Vector2 velocity;
	private Vector2 direction;
	public Vector2 savePoint = new Vector2(107,227);
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private float minHeigth = 310f;
	private bool lookingRight = true;
	private bool doubleJump;
	public int diamante;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D animate;

	public override void _Ready() {
		animate = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	} //fim do ready

	public void ReturnToSavePoint()
	{
		GlobalPosition = savePoint;
	}

	public void ContarDiamantes()
	{
		if(diamante == 4){
			GD.Print("Mapa concluido com Sucesso!");
			diamante = 0;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		//inercia inicial
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Moves();

		ContarDiamantes();
		
		OutOfTheMap();

		Velocity = velocity;
		MoveAndSlide();

		Animation(velocity);

	} //fim do process

	private void Moves()
	{
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			doubleJump = true;
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
			//velocity.X = 0;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		else if (direction != Vector2.Zero && !IsOnFloor())
		{
			if(velocity.X > 0 && direction.X < 0)
			{
				velocity.X = direction.X * (Speed / 3);
			} 
			else if (velocity.X < 0 && direction.X > 0)
			{
				velocity.X = direction.X * (Speed / 3);
			}
			else velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed/2, Speed);
		}
		else if (!IsOnFloor())
		{
			//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
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
			Scale *= new Vector2(-1, 1);
		}
		else if (velocity.X < 0 && !lookingRight)
		{
			lookingRight = true;
			Scale *= new Vector2(-1, 1);
		}
	}

	private void OutOfTheMap()
	{
		if(GlobalPosition.Y > minHeigth)
		{
			ReturnToSavePoint();
		}
	}

} //fim da classe
