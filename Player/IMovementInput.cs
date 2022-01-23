using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IMovementInput
    {
        public float VerticalVelocity();
        public float HorizontalVelocity();
        public float SprintImpact();
    }
}