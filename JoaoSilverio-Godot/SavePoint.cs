using Godot;
using System;

public partial class SavePoint : Area2D
{
	private bool isOnSavePoint;

	public override void _Process(double delta)
	{
		if (isOnSavePoint && Input.IsActionJustPressed("ativar"))
		{
			GetTree().CallGroup("GrupoJogador", "SaveNewSavePoint");
			GD.Print("Save Point Alterado!");
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Jogador)
		{
			isOnSavePoint = true;
			GD.Print("Save Point Pode Ser Alterado!");
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Jogador)
		{
			isOnSavePoint = false;
			GD.Print("Save Point Nao Pode Mais Ser Alterado!");
		}
	}
}


