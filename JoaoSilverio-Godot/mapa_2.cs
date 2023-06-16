using Godot;
using System;

public partial class mapa_2 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().CallGroup("GrupoJogador", "SetNumberOfDiamonds", 3);
		GetTree().CallGroup("GrupoJogador", "SetStart", new Vector2(0, 256));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
