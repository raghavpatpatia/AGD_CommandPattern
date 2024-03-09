using Command.Input;
using Command.Main;
using Command.Player;
using Command.Commands;
using UnityEngine;

namespace Command.Actions
{
    public class HealAction : IAction
    {
        private UnitController actorUnit;
        private UnitController targetUnit;
        private bool IsSuccessful;
        public TargetType TargetType => TargetType.Friendly;

        public void PerformAction(UnitController actorUnit, UnitController targetUnit, bool IsSuccessful)
        {
            this.actorUnit = actorUnit;
            this.targetUnit = targetUnit;
            this.IsSuccessful = IsSuccessful;
            actorUnit.PlayBattleAnimation(CommandType.Heal, CalculateMovePosition(targetUnit), OnActionAnimationCompleted);
        }

        public void OnActionAnimationCompleted()
        {
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.HEAL);

            if (IsSuccessful)
                targetUnit.RestoreHealth(actorUnit.CurrentPower);
        }
        public Vector3 CalculateMovePosition(UnitController targetUnit) => targetUnit.GetEnemyPosition();
    }
}