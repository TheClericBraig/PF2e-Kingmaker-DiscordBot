﻿public class Kingdom
{
    private string KingdomName;
    private string CapitolName;

    private EnumCharter Charter;
    private EnumHeartland Heartland;
    private EnumGovernment Government;

    private int ExperiencePoints;
    private int KingdomLevel;
    private int PlayersLevel;

    private Dictionary<EnumAbilityScore, Ability> Abilities;

    private int FamePoints;
    private int UnrestPoints;

    private int RessourcePoints;
    private Dictionary<EnumCommodity, Commodity> Commodities;

    private List<Leader> Leaders;

    private List<Settlement> Settlements;

    private List<Hex> Territory;
    
    private Dictionary<EnumRuinCategory, int> RuinScore;
    private Dictionary<EnumRuinCategory, int> RuinThreshold;
    private Dictionary<EnumRuinCategory, int> RuinItemPenalty;    

    private Dictionary<EnumSkills, EnumSkillTraining> SkillTrainings;

    private Dictionary<EnumFeats, string> Feats;    

    public enum EnumCharter
    {
        None,
        Conquest,
        Expansion,
        Exploration,
        Grant,
        Open
    }

    public enum EnumHeartland
    {
        None,
        Forest,
        Swamp,
        Hill,
        Plain,
        Lake,
        River,
        Mountain,
        Ruin
    }

    public enum EnumGovernment
    {
        None,
        Despotism,
        Feudalism,
        Oligarchy,
        Republic,
        Thaumocracy,
        Yeomanry
    }

    public enum EnumRuinCategory
    {
        Corruption,
        Crime,
        Strife,
        Decay
    }

    public Kingdom(string name, string capitolName)
    {
        KingdomName = name;
        CapitolName = capitolName;

        Charter = EnumCharter.None;
        Heartland = EnumHeartland.None;
        Government = EnumGovernment.None;        

        KingdomLevel = 1;

        UnrestPoints = 0;

        Abilities = new Dictionary<EnumAbilityScore, Ability>();
        Abilities[EnumAbilityScore.Culture] = new Ability(EnumAbilityScore.Culture);
        Abilities[EnumAbilityScore.Economy] = new Ability(EnumAbilityScore.Economy);
        Abilities[EnumAbilityScore.Loyalty] = new Ability(EnumAbilityScore.Loyalty);
        Abilities[EnumAbilityScore.Stability] = new Ability(EnumAbilityScore.Stability);

        Leaders = new List<Leader>();

        Settlements = new List<Settlement>();
        Hex InitialHex = new Hex(1, 1, Heartland);
        Settlements.Add(new Settlement(capitolName, InitialHex));
        Territory = new List<Hex>();
        Territory.Add(InitialHex);

        SkillTrainings = new Dictionary<EnumSkills, EnumSkillTraining>();

        RuinThreshold = new Dictionary<EnumRuinCategory, int>();
        RuinScore = new Dictionary<EnumRuinCategory, int>();
        RuinItemPenalty = new Dictionary<EnumRuinCategory, int>();

        Feats = new Dictionary<EnumFeats, string>();

        Commodities = new Dictionary<EnumCommodity, Commodity>();
        Commodities[EnumCommodity.Food] = new Commodity(EnumCommodity.Food);
        Commodities[EnumCommodity.Lumber] = new Commodity(EnumCommodity.Lumber);
        Commodities[EnumCommodity.Stone] = new Commodity(EnumCommodity.Stone);
        Commodities[EnumCommodity.Ore] = new Commodity(EnumCommodity.Ore);
        Commodities[EnumCommodity.Luxuries] = new Commodity(EnumCommodity.Luxuries);
    }  
    

    public int KingdomSize()
    {
        return Territory.Count;
    }

    public void AssignCharter(EnumCharter charter)
    {
        if (Charter != EnumCharter.None)
        {
            throw new Exception("Charter already chosen. You can't change your charter.");
        }

        if (charter == EnumCharter.None)
        {
            throw new Exception("You must choose a valid charter.");
        }

        Charter = charter;
    }

    public void AssignHeartland(EnumHeartland heartland)
    {
        if (Heartland != EnumHeartland.None)
        {
            throw new Exception("Heartland already chosen. You can't change your heartland.");
        }

        if (heartland == EnumHeartland.None)
        {
            throw new Exception("You must choose a valid heartland.");
        }

        Heartland = heartland;
    }

    public void AssignGovernment(EnumGovernment government)
    {
        if (Government != EnumGovernment.None)
        {
            throw new Exception("Government already chosen. You can't change your government.");
        }

        if (government == EnumGovernment.None)
        {
            throw new Exception("You must choose a valid government.");
        }

        Government = government;

        switch(Government) 
        {
            case EnumGovernment.Despotism:
                TrainSkill(EnumSkills.Intrigue);
                TrainSkill(EnumSkills.Warfare);
                break;
            case EnumGovernment.Feudalism:
                TrainSkill(EnumSkills.Defense);
                TrainSkill(EnumSkills.Trade);
                break;
            case EnumGovernment.Oligarchy:
                TrainSkill(EnumSkills.Arts);
                TrainSkill(EnumSkills.Industry);
                break;
            case EnumGovernment.Republic:
                TrainSkill(EnumSkills.Engineering);
                TrainSkill(EnumSkills.Politics);
                break;
            case EnumGovernment.Thaumocracy:
                TrainSkill(EnumSkills.Folklore);
                TrainSkill(EnumSkills.Magic);
                break;
            case EnumGovernment.Yeomanry:
                TrainSkill(EnumSkills.Agriculture);
                TrainSkill(EnumSkills.Wilderness);
                break;
        }
    }

    public void AssignFirstLeaderSkill(EnumSkills addedSkill)
    {
        if(SkillTrainings.Count > 2)
        {
            throw new Exception("First leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignSecondLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 3)
        {
            throw new Exception("Second leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignThirdLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 4)
        {
            throw new Exception("Third leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignFourthLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 5)
        {
            throw new Exception("Fourth leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void FinalizeAbilityScore(EnumAbilityScore charterChoice, EnumAbilityScore governmentChoice,
                                        EnumAbilityScore freeChoice1, EnumAbilityScore freeChoice2)
    {
        switch (Charter)
        {
            case EnumCharter.Conquest:
                if (charterChoice == EnumAbilityScore.Stability || charterChoice == EnumAbilityScore.Economy)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility(true);
                break;
            case EnumCharter.Expansion:
                if (charterChoice == EnumAbilityScore.Loyalty || charterChoice == EnumAbilityScore.Economy)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility(true);
                break;
            case EnumCharter.Exploration:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Loyalty)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility(true);
                break;
            case EnumCharter.Grant:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Stability)
                {                
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility(true);
                break;
            case EnumCharter.Open:
                Abilities[charterChoice].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom charter is not valid.");
        }

        switch (Heartland)
        {
            case EnumHeartland.Forest:
            case EnumHeartland.Swamp:
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                break;
            case EnumHeartland.Hill:
            case EnumHeartland.Plain:
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            case EnumHeartland.Lake:
            case EnumHeartland.River:
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumHeartland.Mountain:
            case EnumHeartland.Ruin:
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom heartland is not valid.");
        }

        switch (Government)
        {
            case EnumGovernment.Despotism:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Loyalty)
                {                
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Feudalism:
                if (governmentChoice != EnumAbilityScore.Economy && governmentChoice != EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                break;
            case EnumGovernment.Oligarchy:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Stability)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Republic:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            case EnumGovernment.Thaumocracy:
                if (governmentChoice != EnumAbilityScore.Stability && governmentChoice != EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Yeomanry:
                if (governmentChoice != EnumAbilityScore.Stability && governmentChoice != EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom government is not valid.");
        }

        if (freeChoice1 == freeChoice2)
        {
            throw new Exception("You must choose two different additional ability boosts.");
        }
        Abilities[freeChoice1].BoostAbility();
        Abilities[freeChoice2].BoostAbility();
    }

    public void AddLeader(Leader addedLeader)
    {
        if (addedLeader == null) throw new Exception("You must choose a leader to add.");

        int totalInvested = 0;
        foreach (Leader forLeader in Leaders) 
        {
            if (forLeader.getRole() == addedLeader.getRole()) throw new Exception("There is already a leader in this Role.");

            if(forLeader.getInvested()) totalInvested++;
        }

        if (addedLeader.getInvested() && totalInvested >= 4) throw new Exception("There is already a leader in this Role.");

        Leaders.Add(addedLeader);
    }

    public void setPlayersLevel(int level)
    { this.PlayersLevel = level; }

    public void RemoveLeader(Leader removedLeader) { Leaders.Remove(removedLeader); } //TODO : Test the input. I guess I require an ID.

    public int ControlDC()
    {
        int returnedDC;

        switch (KingdomLevel) 
        {
            case 1: returnedDC = 14; break;
            case 2: returnedDC = 15; break;
            case 3: returnedDC = 16; break;
            case 4: returnedDC = 18; break;
            case 5: returnedDC = 20; break;
            case 6: returnedDC = 22; break;
            case 7: returnedDC = 23; break;
            case 8: returnedDC = 24; break;
            case 9: returnedDC = 26; break;
            case 10: returnedDC = 27; break;                   
            case 11: returnedDC = 28; break;
            case 12: returnedDC = 30; break;
            case 13: returnedDC = 31; break;
            case 14: returnedDC = 32; break;
            case 15: returnedDC = 34; break;
            case 16: returnedDC = 35; break;
            case 17: returnedDC = 36; break;
            case 18: returnedDC = 38; break;
            case 19: returnedDC = 39; break;
            case 20: returnedDC = 40; break;
            default: throw new ArgumentOutOfRangeException("The Kingdom Level must be between 1 and 20.");
        }

        returnedDC += KingdomSizeControlDCModifier();

        return returnedDC;
    }

    public int RessourceDiceSize()
    {        
        if (1 <= KingdomSize() || KingdomSize() <= 9)
        { return 4; }
        else if (10 < KingdomSize() || KingdomSize() < 24)
        { return 6; }
        else if (25 < KingdomSize() || KingdomSize() < 49)
        { return 8; }
        else if (50 < KingdomSize() || KingdomSize() < 99)
        { return 10; }
        else if (KingdomSize() > 100)
        { return 12; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    public int KingdomSizeControlDCModifier()
    {
        if (1 <= KingdomSize() || KingdomSize() <= 9)
        { return 0; }
        else if (10 < KingdomSize() || KingdomSize() < 24)
        { return 1; }
        else if (25 < KingdomSize() || KingdomSize() < 49)
        { return 2; }
        else if (50 < KingdomSize() || KingdomSize() < 99)
        { return 3; }
        else if (KingdomSize() > 100)
        { return 4; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    

   

    public int RessourceDiceAmount()
    {
        int amount = KingdomLevel + 4;

        //TODO : Any reduction goes here.

        return Math.Max(amount, 0);
    }

    public int Consumption()
    {
        return 1; //TODO : Settlement - Farmlands + etc.
    }

    public bool EarnFame()
    {
        int FameThreshold = 3;

        if(KingdomLevel >= 20)
        {
            FameThreshold++;
        }

        if (FamePoints >= FameThreshold)
        {
            return false;
        }

        FamePoints += 1;
        return true;        
    }

    public EnumCheckResult KingdomCheck(int DC, int modifier = 0) 
    {
        EnumCheckResult returnedResult = Utility.MakeCheck(DC, modifier);

        if(returnedResult == EnumCheckResult.CritSuccess) { EarnFame(); }

        return returnedResult;
    }

    public EnumCheckResult UseSkill(EnumSkills usedSkill)
    {
        int totalModifier = 0;
        int proficiencyBonus = 0;
        int statusBonus = 0;
        
        if (SkillTrainings.ContainsKey(usedSkill))
        {
            proficiencyBonus += Skill.TrainingBonus(SkillTrainings[usedSkill]) + KingdomLevel;
        }
        
        if (SkillHasStatusBonusFromInvestedLeader(usedSkill))
        {
            int bonusFromInvested = 1;
            if (KingdomLevel >= 8) bonusFromInvested = 2;
            if (KingdomLevel >= 16) bonusFromInvested = 3;
            statusBonus = Math.Max(statusBonus, bonusFromInvested);
        }

        totalModifier += Abilities[Skill.SkillList()[usedSkill].KeyAbility].Modifier();
        totalModifier += proficiencyBonus;
        totalModifier += statusBonus;
        totalModifier -= RuinItemPenalty[RuinCategoryByAbility(Skill.SkillList()[usedSkill].KeyAbility)];

        return KingdomCheck(ControlDC(), totalModifier);
    }
    
    public void TrainSkill(EnumSkills enumSkill, EnumSkillTraining skillTraining = EnumSkillTraining.Trained)
    {
        
        if(SkillTrainings.ContainsKey(enumSkill) && SkillTrainings[enumSkill] == skillTraining)
        { 
            throw new Exception("This skill is already at ." + skillTraining.ToString());
        }

        SkillTrainings[enumSkill] = skillTraining;
    }

    public bool SkillHasStatusBonusFromInvestedLeader(EnumSkills enumSkill)
    {
        foreach (Leader forLeader in Leaders)
        {
            if(forLeader.getInvested() && Leader.KeyAbilityForRole(forLeader.getRole()) == Skill.SkillList()[enumSkill].KeyAbility)
            {
                return true;
            }
        }

        return false;
    }

    public int UnrestStatusPenalty()
    {
        if (UnrestPoints < 0) throw new Exception("Unrest score can't be negative.");

        if(UnrestPoints >= 15)
        {
            return 4;
        }
        if (UnrestPoints >= 10)
        {
            return 3;
        }
        if (UnrestPoints >= 5)
        {
            return 2;
        }
        if (UnrestPoints >= 1)
        {
            return 1;
        }
        return 0;
    }

    public void AddRuin(int ruinAmount, EnumRuinCategory ruinCategory)
    {
        RuinScore[ruinCategory] += ruinAmount;
        if(RuinScore[ruinCategory] > RuinThreshold[ruinCategory])
        {
            RuinScore[ruinCategory] -= RuinThreshold[ruinCategory];
            RuinItemPenalty[ruinCategory] += 1;
        }

    }

    public static EnumRuinCategory RuinCategoryByAbility(EnumAbilityScore enumAbility)
    {
        switch(enumAbility)
        {
            case EnumAbilityScore.Culture:
                return EnumRuinCategory.Corruption;
            case EnumAbilityScore.Economy:
                return EnumRuinCategory.Crime;
            case EnumAbilityScore.Loyalty:
                return EnumRuinCategory.Strife;
            case EnumAbilityScore.Stability:
                return EnumRuinCategory.Decay;
            default: throw new NotImplementedException("This ability score does'nt exist.");
        }
    }

    public void AddXP(int xpAmount)
    {
        ExperiencePoints += xpAmount;
    }

    public void LevelUp()
    {
        if (ExperiencePoints < 1000)
        {
            return;
        }

        if(KingdomLevel >= PlayersLevel)
        {
            return;
        }

        ExperiencePoints -= 1000;
        KingdomLevel += 1;

        switch(KingdomLevel)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                Feats[EnumFeats.ExpansionExpert] = "";
                Feats[EnumFeats.FineLiving] = "";
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                Feats[EnumFeats.ExpansionExpertPlus] = "";
                break;
            case 10:
                Feats[EnumFeats.LifeOfLuxury] = "";
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                throw new NotSupportedException("You're too OP, stop right there !");
        }
                
    }
}

