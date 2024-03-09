using Command.Input;
using Command.Main;
using Command.Player;
using Command.Commands;
using UnityEngine;

namespace Command.Actions
{
    public class ThirdEyeAction : IAction
    {
        private UnitController actorUnit;
        private UnitController targetUnit;
        private bool IsSuccessful;
        public TargetType TargetType => TargetType.Self;

        public void PerformAction(UnitController actorUnit, UnitController targetUnit, bool IsSuccessful)
        {
            this.actorUnit = actorUnit;
            this.targetUnit = targetUnit;
            this.IsSuccessful = IsSuccessful;
            actorUnit.PlayBattleAnimation(CommandType.BerserkAttack, CalculateMovePosition(targetUnit), OnActionAnimationCompleted);
        }

        public void OnActionAnimationCompleted()
        {
            if (IsSuccessful)
            {
                int healthToConvert = (int)(targetUnit.CurrentHealth * 0.25f);
                targetUnit.TakeDamage(healthToConvert);
                targetUnit.CurrentPower += healthToConvert;
            }
            else
                GameService.Instance.UIService.ActionMissed();
        }

        public Vector3 CalculateMovePosition(UnitController targetUnit) => targetUnit.GetEnemyPosition();
    }
}