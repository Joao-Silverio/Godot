using Godot;
using System;

public partial class Menu : Control
{	
	private void Iniciar()
	{
		GetTree().ChangeSceneToFile("res://mapa_JoaoSilverio.tscn");
	}
	
	private void Créditos()
	{
		GetTree().ChangeSceneToFile("res://Creditos.tscn");
	}
	
	private void Sair()
	{
		GetTree().Quit();
	}
}


