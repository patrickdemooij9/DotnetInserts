using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.TcgStorage
{
    public class MarvelChampionsCardGameBox
    {
        public void Render()
        {
            var insert = new Insert("MarvelChampionsBox");

            var divider = new CardDivider(new CardDividerDataModel
            {
                Size = new Vector3(2, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ),
                WallSize = 1.5,
                BottomCutoutSize = new Vector3(2.5, 20, 2)
            });
            var box = new InsertBox(new Vector3(250, 150, Constants.CardSizeSleevedZ + 6))
            {
                Lid = new InsertBoxLidDataModel(3, 3)
                {
                    ImagePath = "MarvelChampions.svg"
                }
            };
            var tray = new CardTrayComponent(new CardTrayDataModel(new Vector3(250 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 18));
            var secondTray = new CardTrayComponent(new CardTrayDataModel(new Vector3(250 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 18));
            box.Components.Add(tray, new ComponentLocation(new Vector3(0, box.Size.Y / 2 - 4 - tray.Model.Size.Y / 2, 0)));
            box.Components.Add(secondTray, new ComponentLocation(new Vector3(0, -(box.Size.Y / 2 - 4 - tray.Model.Size.Y / 2), 0)));

            //RenderAllDividerTypes(tray.GetDivider().WithTolerance());
            //RenderAllLidTypes(box.GetLid());

            tray.GetDivider().WithTolerance().ToFile("Divider.scad");

            insert.Parts.Add(box, new Vector3());
            var lid = box.GetLid();
            insert.Parts.Add(lid.WithTolerance(), new Vector3(0, 0, box.Size.Z / 2));
            insert.Parts.Add(lid.GetImage(), new Vector3(0, 0, box.Size.Z / 2));
            insert.Render(out _);
        }

        void RenderAllLidTypes(InsertBoxLid lid)
        {
            var starWarsUnlimitedLid = (InsertBoxLid)lid.Clone();
            starWarsUnlimitedLid.Model.ImagePath = "StarWarsUnlimited.svg";
            starWarsUnlimitedLid.WithTolerance().ToFile("StarWarsUnlimitedLid.scad").CreateModel("StarWarsUnlimitedLid.stl");
            starWarsUnlimitedLid.GetImage().ToFile("StarWarsUnlimitedLid2.scad").CreateModel("StarWarsUnlimitedLid2.stl");

            var aggressionLid = (InsertBoxLid)lid.Clone();
            aggressionLid.Model.ImagePath = "SWU_Aggression.svg";
            aggressionLid.WithTolerance().ToFile("AggressionLid.scad").CreateModel("AggressionLid.stl");
            aggressionLid.GetImage().ToFile("AggressionLid2.scad").CreateModel("AggressionLid2.stl");

            var commandLid = (InsertBoxLid)lid.Clone();
            commandLid.Model.ImagePath = "SWU_Command.svg";
            commandLid.WithTolerance().ToFile("CommandLid.scad").CreateModel("CommandLid.stl");
            commandLid.GetImage().ToFile("CommandLid2.scad").CreateModel("CommandLid2.stl");

            var cunningLid = (InsertBoxLid)lid.Clone();
            cunningLid.Model.ImagePath = "SWU_Cunning.svg";
            cunningLid.WithTolerance().ToFile("CunningLid.scad").CreateModel("CunningLid.stl");
            cunningLid.GetImage().ToFile("CunningLid2.scad").CreateModel("CunningLid2.stl");

            var heroismLid = (InsertBoxLid)lid.Clone();
            heroismLid.Model.ImagePath = "SWU_Heroism.svg";
            heroismLid.WithTolerance().ToFile("HeroismLid.scad").CreateModel("HeroismLid.stl");
            heroismLid.GetImage().ToFile("HeroismLid2.scad").CreateModel("HeroismLid2.stl");

            var vigilanceLid = (InsertBoxLid)lid.Clone();
            vigilanceLid.Model.ImagePath = "SWU_Vigilance.svg";
            vigilanceLid.WithTolerance().ToFile("VigilanceLid.scad").CreateModel("VigilanceLid.stl");
            vigilanceLid.GetImage().ToFile("VigilanceLid2.scad").CreateModel("VigilanceLid2.stl");

            var villainyLid = (InsertBoxLid)lid.Clone();
            villainyLid.Model.ImagePath = "SWU_Villainy.svg";
            villainyLid.WithTolerance().ToFile("VillainyLid.scad").CreateModel("VillainyLid.stl");
            villainyLid.GetImage().ToFile("VillainyLid2.scad").CreateModel("VillainyLid2.stl");
        }

        void RenderAllDividerTypes(CardDivider mainDivider)
        {
            var commandDivider = (CardDivider) mainDivider.Clone();
            commandDivider.Model.ImagePath = "SWU_Command.svg";
            commandDivider.ToFile("CommandDivider.scad").CreateModel("CommandDivider.stl");

            var aggressionDivider = (CardDivider)mainDivider.Clone();
            aggressionDivider.Model.ImagePath = "SWU_Aggression.svg";
            aggressionDivider.ToFile("AggressionDivider.scad").CreateModel("AggressionDivider.stl");

            var cunningDivider = (CardDivider)mainDivider.Clone();
            cunningDivider.Model.ImagePath = "SWU_Cunning.svg";
            cunningDivider.ToFile("CunningDivider.scad").CreateModel("CunningDivider.stl");

            var heroismDivider = (CardDivider)mainDivider.Clone();
            heroismDivider.Model.ImagePath = "SWU_Heroism.svg";
            heroismDivider.ToFile("HeroismDivider.scad").CreateModel("HeroismDivider.stl");

            var vigilanceDivider = (CardDivider)mainDivider.Clone();
            vigilanceDivider.Model.ImagePath = "SWU_Vigilance.svg";
            vigilanceDivider.ToFile("VigilanceDivider.scad").CreateModel("VigilanceDivider.stl");

            var villainyDivider = (CardDivider)mainDivider.Clone();
            villainyDivider.Model.ImagePath = "SWU_Villainy.svg";
            villainyDivider.ToFile("VillainyDivider.scad").CreateModel("VillainyDivider.stl");

            var uniqueDivider = (CardDivider)mainDivider.Clone();
            uniqueDivider.Model.ImagePath = string.Empty;
            uniqueDivider.ToFile("UniqueDivider.scad").CreateModel("UniqueDivider.stl");
        }
    }
}
