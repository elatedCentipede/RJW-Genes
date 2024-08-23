﻿using Verse;
using rjw;
using RimWorld;

namespace RJW_Genes
{
    public class Gene_NoPenis : RJW_Gene
    {

        internal Hediff removed_penis;
        internal Hediff missing_bodypart_hediff;

        public override void PostMake()
        {
            base.PostMake();

            // Penis are only removed for male pawns!
            if (GenderUtility.IsMale(pawn) && removed_penis == null)
            {
                RemoveButStorePenis();
            }
        }
        
        public override void PostAdd()
        {
            base.PostAdd();

            // Penis are only removed for male pawns!
            if (GenderUtility.IsMale(pawn) && removed_penis == null)
            {
                RemoveButStorePenis();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();

            if (missing_bodypart_hediff != null)
                pawn.health.RemoveHediff(missing_bodypart_hediff);

            if (removed_penis != null)    
                pawn.health.AddHediff(removed_penis);
        }

        internal void RemoveButStorePenis()
        {
            var partBPR = Genital_Helper.get_genitalsBPR(pawn);
            Hediff penisToRemove = Genital_Helper.get_AllPartsHediffList(pawn).FindLast(x => Genital_Helper.is_penis(x));

            if(penisToRemove != null)
            {
                removed_penis = penisToRemove;
                pawn.health.RemoveHediff(penisToRemove);

                if (missing_bodypart_hediff == null)
                    missing_bodypart_hediff = pawn.health.AddHediff(RimWorld.HediffDefOf.MissingBodyPart, partBPR);
            }
        }

    }
}
