using Godot;
using HelloWorld.Core;
using Massive.Common;
using Massive.Netcode;

namespace HelloWorld.Client.core;

public partial class ServerGameRunner : Node {
	public Server Server;
	public double Time;

	public override void _Ready() {
		Server = new Server(new SessionConfig(), new TcpConnectionsListener(1987));
		Server.InputIdentifiers.RegisterAutomaticallyFromAllAssemblies();

		new GameSetup().SetupGame(Server.Session.Systems, Server.Session.World, 0, 0);
		
		Server.Session.Systems.Build(Server.Session);

		var systemsSimulation = new SystemsSimulation(Server.Session.Systems);
    
		Server.Session.Simulations.Add(systemsSimulation);
    
		systemsSimulation.Initialize();
    
		Server.ConnectionListener.Start();
	}

	public override void _PhysicsProcess(double delta) {
		Time += delta;
		Server.Update(Time);
	}
}