using Data;
using StaticData;

namespace Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly GameConfig _gameConfig;

        public GameFactory(GameConfig gameConfig) =>
            _gameConfig = gameConfig;

        public Door CreateDoor()
        {
            int pin1 = _gameConfig.DoorPins[0];
            int pin2 = _gameConfig.DoorPins[1];
            int pin3 = _gameConfig.DoorPins[2];
            var door = new Door(pin1, pin2, pin3);
            ApplyEncoding(door);
            return door;
        }

        private void ApplyEncoding(Door door)
        {
            foreach (int toolIndex in _gameConfig.Key)
                door.ApplyTool(_gameConfig.Tools[toolIndex], -1);
        }
    }
}