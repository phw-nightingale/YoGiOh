using System;
using Repository;
using Utility.Singleton;

namespace Management
{
    public class GameManager : MonoSingletonManual<GameManager>
    {
        private static CardRepository _cardRepository;
        
        protected override void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _cardRepository = CardRepository.Instance;
        }
    }
}