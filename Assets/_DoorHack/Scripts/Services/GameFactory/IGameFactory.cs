using Data;

namespace Services.GameFactory
{
    public interface IGameFactory : IService
    {
        Door CreateDoor();
    }

}