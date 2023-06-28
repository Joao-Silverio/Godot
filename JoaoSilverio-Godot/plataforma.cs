using Godot;
using System;

public partial class plataforma : Area2D
{
	private StaticBody2D plat;
	private Timer tempo;
	private bool pisou = false;
	private bool sumiu = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tempo = GetNode<Timer>("Timer");
		plat = GetNode<StaticBody2D>("StaticBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(tempo.TimeLeft < 0.1f && pisou){
			pisou = false;
			sumiu = true;
			RemoveChild(plat); //Retira a imagem da nuvem
			tempo.Start(5);
		} else if (tempo.TimeLeft < 0.1f && sumiu){
			AddChild(plat); //Adiciona a imagem
			sumiu = false;
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body is Jogador){
			tempo.Start(5);
			pisou = true;
		}
	}
}

