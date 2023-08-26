using DL.Domain.Request;

namespace DL.Service.Interface
{
    public interface IFightService
    {
        public void Start(FightServiceRequest request);
    }
}