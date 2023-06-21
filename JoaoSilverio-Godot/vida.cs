using Godot;
using System;

public partial class vida : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			if(((Jogador)body).lives < 3)
			{
				QueueFree();
				((Jogador)body).lives++;
				GD.Print("Vida Coletada");
			}
			
		}
	}
}
