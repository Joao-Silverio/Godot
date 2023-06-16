using Godot;
using System;

public partial class chegada : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).finalPosition = true;
			GD.Print("Posicao final alcançada!");
		}
	}
}
