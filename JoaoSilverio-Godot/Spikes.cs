using Godot;
using System;

public partial class Spikes : Area2D
{
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).ReturnToSavePoint();
			((Jogador)body).lives--;
			GD.Print("Espetado!");
		}
	}
}
