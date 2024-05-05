using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedrunPractice.Extensions.ILData
{
    public class SnakemouthRight : Snakemouth
    {
        public SnakemouthRight(List<Split> splits) : base(splits)
        {
        }

        public override void SetupData()
        {
            base.SetupData();
            ilType = IL.SnakemouthRight;
        }

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Lake Room", MainManager.Maps.SnakemouthLake, TypeSplit.Room),
                new Split("Big Door", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Right A", MainManager.Maps.SnakemouthUndergroundRightA, TypeSplit.Room),
                new Split("Right B", MainManager.Maps.SnakemouthUndergroundRightB, TypeSplit.Room),
                new Split("Big Door 2", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Left A", MainManager.Maps.SnakemouthUndergroundLeftA, TypeSplit.Room),
                new Split("Left B", MainManager.Maps.SnakemouthUndergroundLeftB, TypeSplit.Room),
                new Split("Big Door 3", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Mushrooom Pit", MainManager.Maps.SnakemouthMushroomPit, TypeSplit.Room,endID: MainManager.Maps.SnakemouthTreasureRoom)
            };
        }
    }
}
