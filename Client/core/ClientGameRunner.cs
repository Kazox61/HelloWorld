using System.Net;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using HelloWorld.Core;
using HelloWorld.Core.Input;
using Massive.Common;
using Massive.Netcode;

namespace HelloWorld.Client.core;

public partial class ClientGameRunner : Node {
	public GodotViewSynchronizer GodotViewSynchronizer;
	public Massive.Netcode.Client Client;
	public float ClientTime;
	public IGameSetup GameSetup;
	public Session Session => Client.Session;

	public static int LocalPlayerChannel;

	public override void _EnterTree() {
		GodotViewSynchronizer = new GodotViewSynchronizer();
		
		Client = new Massive.Netcode.Client(new SessionConfig(), new TcpConnection());
		Client.InputIdentifiers.RegisterAutomaticallyFromAllAssemblies();

		GameSetup = new GameSetup();

		GameSetup.SetupGame(Session.Systems, Session.World, 0);

		Session.Systems.Build(Session);

		var basicSimulation = new BasicSimulation(Session.Systems);

		Session.Simulations.Add(basicSimulation);

		// basicSimulation.Initialize();

		Client.Connection.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1987));
	}

	public override void _PhysicsProcess(double delta) {
		ClientTime += (float)delta;

		var playerInput = CollectInput();

		Client.Session.Inputs.SetPredictionInputAt(
			Client.InputPredictionTick(ClientTime),
			Client.Connection.Channel,
			playerInput
		);

		Client.Update(ClientTime);
		LocalPlayerChannel = Client.Connection.Channel;
		
		GodotViewSynchronizer.SynchronizeAll(Session.World);
	}
	
	private PlayerInput CollectInput() {
		var inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_back").Normalized();
		var kill = Input.IsActionJustPressed("ui_cancel");
		var jump = Input.IsActionJustPressed("jump");
		var attack = Input.IsActionJustPressed("shoot");
		return new PlayerInput {
			DirectionX = inputDirection.X,
			DirectionY = inputDirection.Y,
			Kill = kill,
			Jump = jump,
			Attack = attack
		};
	}
}