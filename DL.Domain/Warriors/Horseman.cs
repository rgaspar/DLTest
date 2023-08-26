using DL.Common.Extensions;

namespace DL.Domain.Warriors
{
    public class Horseman : Warrior
    {
        public override void Fight(Warrior attacker)
        {
            new Switch(attacker)
                .Case<Archer>(action =>
                {
                    Random random = new Random();
                    if (random.Next(0, 100) <= 40)
                    {
                        ReCalculateHealth();
                    }
                })
                .Case<Swordsman>(action =>
                {
                    // Nothing happening
                })
                .Case<Horseman>(action =>
                {
                    ReCalculateHealth();
                });
        }
    }
}
