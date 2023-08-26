using DL.Domain.Request;
using DL.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DL.Service
{
    public class FightService : IFightService
    {
        private readonly ILogger<FightService> _log;
        private readonly IConfiguration _config;
        private readonly IWarriorsService _warriorsService;

        public FightService(IWarriorsService warriorsService, ILogger<FightService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            _warriorsService = warriorsService;
        }

        public void Start(FightServiceRequest request)
        {
            var round = 1;
            var randomWarrior = new Random();            

            _log.LogInformation("START FIGHT");
            try
            {
                _log.LogDebug($"----------------------------------------------------------------------------------------");
                var warriors = _warriorsService.GetWarriors(request.WarriorCount);
                _log.LogDebug($"----------------------------------------------------------------------------------------");
                var countOfHealtyWarriors = warriors.Where(w => w.Alive).Count();

                while (countOfHealtyWarriors > 1)
                {
                    _log.LogDebug($"{round} kör -----------------------------------------------------------------------------------");

                    var attacker = warriors.Where(w => w.Alive).OrderBy(x => randomWarrior.Next(0, warriors.Count)).Take(1).First();
                    var defender = warriors.Where(w => w.Alive).OrderBy(x => randomWarrior.Next(0, warriors.Count)).Take(1).First();
                    
                    while(defender.Id == attacker.Id)
                    {
                        defender = warriors.OrderBy(x => randomWarrior.Next(0, warriors.Count)).Take(1).First();
                    }

                    _log.LogInformation($"ATTACKER : {attacker.Name} ({attacker.GetType().Name})");
                    _log.LogInformation($"DEFENDER : {defender.Name} ({defender.GetType().Name})");

                    defender.Fight(attacker);
                    _log.LogInformation($"{attacker.Name}: {attacker.Health} - {defender.Name}: {defender.Health}");
                    if(!attacker.Alive)
                    {
                        _log.LogCritical($"{attacker.Name} died");
                    }
                    if (!defender.Alive)
                    {
                        _log.LogCritical($"{defender.Name} died");
                    }

                    countOfHealtyWarriors = warriors.Where(w => w.Alive).Count();

                    warriors.Where(w => w.Id != attacker.Id && w.Id != defender.Id && w.Alive).ToList().ForEach(f => f.RestTime());

                    round++;
                }
                _log.LogWarning($"Winner : {warriors.SingleOrDefault(w => w.Alive).Name}");
            }
            catch (Exception ex)
            {

                throw;
            }
            _log.LogInformation("END FIGHT");
        }
    }
}