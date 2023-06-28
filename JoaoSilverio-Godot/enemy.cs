using Godot;
using System;

public partial class enemy : Area2D
{
	private int x  = 0;
	private bool direita = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GetNode<Sprite2D>("Sprite2D").Play("default");
		if(direita){
			GlobalPosition = GlobalPosition + new Vector2(1,0);
		}
		else {
			GlobalPosition = GlobalPosition - new Vector2(1,0);
		}
		
		if(x >= 200){
			direita = !direita;
			x = 0;
		}
		
		x++;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).ReturnToSavePoint();
			((Jogador)body).lives--;
			GD.Print("Inimigo Matou Voce!");
		}
	}
}


