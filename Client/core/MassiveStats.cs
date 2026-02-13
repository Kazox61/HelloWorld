using Godot;

namespace HelloWorld.Client.core;

public partial class MassiveStats : CanvasLayer {
	[Export] private Label _inputChannelLabel;
	[Export] private Label _entityCountLabel;
	[Export] private Label _rttLabel;
	[Export] private Label _approvedSimulationTickLabel;
	[Export] private Label _inputPredictionTick;

	public override void _Process(double delta) {
		if (ClientGameRunner.Instance.Client != null) {
			_inputChannelLabel.Text = ClientGameRunner.Instance.LocalPlayerChannel.ToString();
			_rttLabel.Text = (ClientGameRunner.Instance.Client.Rtt * 1000).ToString("0.00") + "ms";
			_approvedSimulationTickLabel.Text = ClientGameRunner.Instance.Client.TickSync.ApprovedSimulationTick.ToString();
			_inputPredictionTick.Text = ClientGameRunner.Instance.Client.InputPredictionTick(ClientGameRunner.Instance.ClientTime).ToString();
		}
		
		if (ClientGameRunner.Instance.Session != null) {
			_entityCountLabel.Text = ClientGameRunner.Instance.Session.World.Entities.Count.ToString();
		}
	}
}