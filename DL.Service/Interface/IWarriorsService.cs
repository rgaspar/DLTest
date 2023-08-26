using DL.Domain;

namespace DL.Service.Interface
{
    public interface IWarriorsService
    {
        public List<Warrior> GetWarriors(int count);
    }
}
