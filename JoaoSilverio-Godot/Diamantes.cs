using Godot;
using System;

public partial class Diamantes : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			QueueFree();
			((Jogador)body).diamante++;
			GD.Print("Diamante Coletado!");
		}
	}
}
