using Godot;
using System;

public partial class SavePoint : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).isOnSavePoint = true;
			GD.Print("Save Point Pode Ser Alterado!");
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Jogador)
		{
			((Jogador)body).isOnSavePoint = false;
			GD.Print("Save Point Nao Pode Mais Ser Alterado!");
		}
	}
}


