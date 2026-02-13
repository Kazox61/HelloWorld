using System.Diagnostics;
using HelloWorld.Core;

var serverGame = new ServerGame();
serverGame.Start();

var stopwatch = new Stopwatch();
stopwatch.Start();
var lastTime = stopwatch.Elapsed.TotalSeconds;

while (true) {
	var currentTime = stopwatch.Elapsed.TotalSeconds;
	var deltaTime = currentTime - lastTime;
	lastTime = currentTime;
	serverGame.Update(deltaTime);
}
