using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990.Application.Interfaces;
using Tanks1990.Game.GameEntities;

namespace Tanks1990.Application.Game.Logic.GameEntities
{
    class GameEntityBuilderFabric
    {
        private uint counter;
        public GameEntityBuilderFabric()
        {
            counter = 0;
        }
        public Dictionary<string, IEntityBuilder> Builders { get; set; }
        private IEntityBuilder builder;
        public IGameEntity Build() {
            var t = builder.Build();
            t.GUID = counter++;
            return t;
        }
        public void ChangeBuilder(IEntityBuilder newBuilder) {
            builder = newBuilder;
        }

    }
}
