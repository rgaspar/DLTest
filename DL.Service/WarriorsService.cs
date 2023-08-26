using DL.Common.Helper;
using DL.Domain;
using DL.Domain.Configuration;
using DL.Domain.Warriors;
using DL.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DL.Service
{
    public class WarriorsService : IWarriorsService
    {
        private readonly ILogger<FightService> _log;
        private readonly IConfiguration _config;
        private readonly IOptionsSnapshot<WarriorConfig> _warriorConfig;
        private readonly IOptionsSnapshot<NameConfig> _nameConfig;

        public WarriorsService(ILogger<FightService> log, IOptionsSnapshot<WarriorConfig> warriorConfig, IOptionsSnapshot<NameConfig> nameConfig, IConfiguration config)
        {
            _log = log;
            _config = config;
            _warriorConfig = warriorConfig;
            _nameConfig = nameConfig;
        }

        public List<Warrior> GetWarriors(int count)
        {
            _log.LogInformation("Warriors creation start");

            var warriors = new List<Warrior>();
            List<string> names = new List<string>();

            try
            {
                _log.LogInformation($"Create {count} warriors!");

                var nameGenerator = new NameGenerator(_nameConfig.Value.Girls, _nameConfig.Value.Boys, _nameConfig.Value.Last);
                names = nameGenerator.RandomNames(count, 0);

                int idx = 0;
                while (warriors.Count < count)
                {
                    warriors.Add(GenerateWarrior(names[idx]));
                    idx++;
                }

                foreach (var item in warriors)
                {
                    _log.LogInformation($"{item.GetType().Name} - {item.Health} - {item.Name}");
                }


            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
            _log.LogInformation("Warriors creation end");
            return warriors;
        }

        private Warrior GenerateWarrior(string name)
        {
            var rand = new Random();
            var rnd = rand.Next(0, 3);
            if (rnd == 0)
            {
                return new Archer() { Health = _warriorConfig.Value.ArcherHealth, MaxHealth = _warriorConfig.Value.ArcherHealth, Name = name };
            }
            else if (rnd == 1)
            {
                return new Swordsman() { Health = _warriorConfig.Value.SwordsmanHealth, MaxHealth = _warriorConfig.Value.SwordsmanHealth, Name = name };
            }
            else
            {
                return new Horseman() { Health = _warriorConfig.Value.HorsemanHealth, MaxHealth = _warriorConfig.Value.HorsemanHealth, Name = name };
            }
        }
    }
}
