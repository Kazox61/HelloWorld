using System.Net;
using Godot;
using HelloWorld.Core;
using HelloWorld.Core.Input;
using Massive.Common;
using Massive.Netcode;
using massivegodotintegration.addons.massive_godot_integration.synchronizer;

namespace HelloWorld.Client.core;

public partial class ClientGameRunner : Node {
	public GodotEntitySynchronization GodotEntitySynchronization;
	public Massive.Netcode.Client Client;
	public float ClientTime;
	public IGameSetup GameSetup;
	public Session Session => Client.Session;

	public override void _EnterTree() {
		Callable.From(Start).CallDeferred();
	}

	private void Start() {
		Client = new Massive.Netcode.Client(new SessionConfig(), new TcpConnection());
		Client.InputIdentifiers.RegisterAutomaticallyFromAllAssemblies();
		
		GodotEntitySynchronization = new GodotEntitySynchronization(Session.World);
		GodotEntitySynchronization.SubscribeViews();

		GameSetup = new GameSetup();

		GameSetup.SetupGame(Session.Systems, Session.World, 0, 0);

		Session.Systems.Build(Session);

		var systemsSimulation = new SystemsSimulation(Session.Systems);

		Session.Simulations.Add(systemsSimulation);

		systemsSimulation.Initialize();

		Client.Connection.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1987));
	}

	public override void _PhysicsProcess(double delta) {
		if (Client == null) {
			return;
		}
		
		ClientTime += (float)delta;

		var desiredTick = Client.TickSync.CalculateTargetTick(ClientTime);
		var playerInput = CollectInput();

		Client.Session.Inputs.SetPredictionInputAt(
			desiredTick,
			Client.Connection.Channel,
			playerInput
		);

		Client.Update(ClientTime);
		GodotEntitySynchronization?.SynchronizeViews();
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