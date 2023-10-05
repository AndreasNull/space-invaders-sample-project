using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerInputState
    {
        public bool isMovingLeft { get; set; }

        public bool isMovingRight { get; set; }

        public bool isFiring { get; set; }
    }
}