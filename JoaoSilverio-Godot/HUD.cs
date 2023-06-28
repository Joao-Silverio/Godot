using Godot;
using System;

public partial class HUD : CanvasLayer
{
	Sprite2D vida1;
	Sprite2D vida2 ;
	Sprite2D vida3; 
	Sprite2D diamante; 
	Label contaDiamante; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		vida1 = GetNode<Sprite2D>("Vida1");
		vida2 = GetNode<Sprite2D>("Vida2");
		vida3 = GetNode<Sprite2D>("Vida3");
		diamante = GetNode<Sprite2D>("Diamante");
		contaDiamante = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void HideVida2()
	{
		vida2.Hide();	
	}

	public void HideVida3()
	{
		vida3.Hide();	
	}

	public void ShowVida1()
	{
		vida1.Show();	
	}

	public void ShowVida2()
	{
		vida2.Show();	
	}

	public void ShowVida3()
	{
		vida3.Show();	
	}

	public void CountDiamonds(string diamonds)
	{
		contaDiamante.Text = diamonds;
	}
}
