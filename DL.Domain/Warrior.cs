namespace DL.Domain
{
    public abstract class Warrior
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public bool Alive { get; set; }

        public Warrior()
        {
            Id = DateTime.Now.Ticks;
            Name = string.Empty;
            Alive = true;
        }

        public bool IsAlive() => Alive;        

        public abstract void Fight(Warrior attacker);

        public virtual void ReCalculateHealth()
        {
            Health /= 2;

            if (Health <= MaxHealth / 4)
            {
                Alive = false;
            }
        }

        public virtual void RestTime()
        {
            if(Health <= MaxHealth - 10)
            {
                Health += 10;
            }
        }
    }
}