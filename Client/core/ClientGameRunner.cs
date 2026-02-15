using System.Net;
using Fixed64;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using HelloWorld.Core;
using HelloWorld.Core.Input;
using Massive.Common;
using Massive.Netcode;

namespace HelloWorld.Client.core;

public partial class ClientGameRunner : Node {
	public static ClientGameRunner Instance { get; private set; }
	
	[Export] public Button JoinButton;
	[Export] public InputCollector InputCollector;
	
	public GodotViewSynchronizer GodotViewSynchronizer { get; private set; }
	public Massive.Netcode.Client Client { get; private set; }
	public float ClientTime { get; private set; }
	public IGameSetup GameSetup { get; private set; }
	public Session Session => Client?.Session;

	public int LocalPlayerChannel => Client?.Connection.Channel ?? 0;

	public override void _EnterTree() {
		Instance = this;
		GodotViewSynchronizer = new GodotViewSynchronizer();
		
		JoinButton.Pressed += JoinGame;
	}

	public override void _ExitTree() {
		Instance = null;
		JoinButton.Pressed -= JoinGame;
	}

	private void JoinGame() {
		Client = new Massive.Netcode.Client(new SessionConfig(), new TcpConnection(), 0.1);
		Client.InputIdentifiers.RegisterAutomaticallyFromAllAssemblies();

		GameSetup = new GameSetup();

		GameSetup.SetupGame(Session.Systems, Session.World, 0);

		Session.Systems.Build(Session);

		var basicSimulation = new BasicSimulation(Session.Systems);

		Session.Simulations.Add(basicSimulation);

		// basicSimulation.Initialize();

		Client.Connection.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2736));
	}

	public override void _PhysicsProcess(double delta) {
		if (Client == null || !Client.Connection.IsConnected) {
			return;
		}
		
		JoinButton.Hide();
		
		ClientTime += (float)delta;

		var playerInput = new PlayerInput {
			MoveDirectionX = InputCollector.MoveDirection.X.ToFP(),
			MoveDirectionY = InputCollector.MoveDirection.Y.ToFP(),
			AimDirectionX = InputCollector.AimDirection.X.ToFP(),
			AimDirectionY = InputCollector.AimDirection.Y.ToFP(),
			Jump = InputCollector.IsJumping,
			Attack = InputCollector.IsAttacking
		};

		Client.Session.Inputs.SetPredictionInputAt(
			Client.InputPredictionTick(ClientTime),
			Client.Connection.Channel,
			playerInput
		);

		Client.Update(ClientTime);
		
		GodotViewSynchronizer.SynchronizeAll(Session.World);
	}
}