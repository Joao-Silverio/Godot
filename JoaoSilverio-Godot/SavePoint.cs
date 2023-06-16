using Godot;
using System;

public partial class SavePoint : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).savePoint = new Vector2(660, 227);
			GD.Print("Save Point Alterado!");
		}
	}
}


