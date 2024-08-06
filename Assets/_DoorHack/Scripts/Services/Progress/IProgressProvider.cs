using Data;

namespace Services.Progress
{
    public interface IProgressProvider : IService
    {
        PlayerProgress Progress { get; set; }
    }
}