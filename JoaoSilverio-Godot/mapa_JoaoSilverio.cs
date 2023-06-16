using Godot;
using System;
using System.Collections.Specialized;

public partial class mapa_JoaoSilverio : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().CallGroup("GrupoJogador", "SetNumberOfDiamonds", 4);
		GetTree().CallGroup("GrupoJogador", "SetStart", new Vector2(107,230));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
