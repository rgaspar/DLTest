using DL.Common.Extensions;

namespace DL.Domain.Warriors
{
    public class Archer : Warrior
    {

        public override void Fight(Warrior attacker)
        {
            new Switch(attacker)
                .Case<Archer>(action =>
                {
                    ReCalculateHealth();
                })
                .Case<Swordsman>(action =>
                {
                    ReCalculateHealth();
                })
                .Case<Horseman>(action =>
                {
                    ReCalculateHealth();
                });
        }
    }
}
