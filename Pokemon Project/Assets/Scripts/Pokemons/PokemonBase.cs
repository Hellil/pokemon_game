using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite shinyFrontSprite;
    [SerializeField] Sprite shinyBackSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] int expYield;
    [SerializeField] GrowthRate growthRate;

    [SerializeField] int catchRate = 255;

    [SerializeField] List<LearnableMove> learnableMoves;
    [SerializeField] List<MoveBase> learnableByItems;

    [SerializeField] List<Evolution> evolutions;

    public static int MaxNumOfMoves { get; set; } = 4;

    public int GetExpForLevel(int level)
    {
        if (growthRate == GrowthRate.Max)
        {
            return 31 * (level * level * level) / 50;
        }
        else if (growthRate == GrowthRate.Fast)
        {
            return 4 * (level * level * level) / 5;
        }
        else if (growthRate == GrowthRate.MediumFast)
        {
            return (level * level * level);
        }
        else if (growthRate == GrowthRate.MediumSlow)
        {
            return (6 * (level * level * level) / 5) - (15 * (level * level)) + (100 * level) - 140;
        }
        else if (growthRate == GrowthRate.Slow)
        {
            return 5 * (level * level * level) / 4;
        }
        else if (growthRate == GrowthRate.Min)
        {
            return 3 * (level * level * level) / 2;
        }

        return -1;
    }

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public Sprite ShinyFrontSprite
    {
        get { return shinyFrontSprite; }
    }

    public Sprite ShinyBackSprite
    {
        get { return shinyBackSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    { 
        get { return learnableMoves; }
    }

    public List<MoveBase> LearnableByItems => learnableByItems;

    public List<Evolution> Evolutions => evolutions;

    // same as previous getters
    public int CatchRate => catchRate;

    public int ExpYield => expYield;

    public GrowthRate GrowthRate => growthRate;
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

[System.Serializable]
public class Evolution
{
    [SerializeField] PokemonBase evolvesInto;
    [SerializeField] int requiredLevel;
    [SerializeField] EvolutionItem requiredItem;

    public PokemonBase EvolvesInto => evolvesInto;
    public int RequiredLevel => requiredLevel;
    public EvolutionItem RequiredItem => requiredItem;
}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}

public enum GrowthRate
{
    Max, Fast, MediumFast, MediumSlow, Slow, Min
}

public enum Stat
{
    Attack,
    Defense,
    SpAttack,
    SpDefense,
    Speed,

    Accuracy,
    Evasion
}

public class TypeChart
{
    static float[][] chart =
    {
        /* attacker\defender  NOR   FIR   WAT   GRA   ELE   ICE   FIG   POI   GRO   FLY   PSY   BUG   ROC   GHO   DRA   DAR   STE   FAI*/
        /*NOR*/ new float[] { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   0.5f, 0f,   1f,   1f,   0.5f, 1f },
        /*FIR*/ new float[] { 1f,   0.5f, 0.5f, 2f,   1f,   2f,   1f,   1f,   1f,   1f,   1f,   2f,   0.5f, 1f,   0.5f, 1f,   2f,   1f },
        /*WAT*/ new float[] { 1f,   2f,   0.5f, 0.5f, 1f,   1f,   1f,   1f,   2f,   1f,   1f,   1f,   2f,   1f,   0.5f, 1f,   1f,   1f },
        /*GRA*/ new float[] { 1f,   0.5f, 2f,   0.5f, 1f,   1f,   1f,   0.5f, 2f,   0.5f, 1f,   0.5f, 2f,   1f,   0.5f, 1f,   0.5f, 1f },
        /*ELE*/ new float[] { 1f,   1f,   2f,   0.5f, 0.5f, 1f,   1f,   1f,   0f,   2f,   1f,   1f,   1f,   1f,   0.5f, 1f,   1f,   1f },
        /*ICE*/ new float[] { 1f,   0.5f, 0.5f, 2f,   1f,   0.5f, 1f,   1f,   2f,   2f,   1f,   1f,   1f,   1f,   2f,   1f,   0.5f, 1f },
        /*FIG*/ new float[] { 2f,   1f,   1f,   1f,   1f,   2f,   1f,   0.5f, 1f,   0.5f, 0.5f, 0.5f, 2f,   0f,   1f,   2f,   2f,   0.5f },
        /*POI*/ new float[] { 1f,   1f,   1f,   2f,   1f,   1f,   1f,   0.5f, 0.5f, 1f,   1f,   1f,   0.5f, 0.5f, 1f,   1f,   0f,   2f },
        /*GRO*/ new float[] { 1f,   2f,   1f,   0.5f, 2f,   1f,   1f,   2f,   1f,   0f,   1f,   0.5f, 2f,   1f,   1f,   1f,   2f,   1f },
        /*FLY*/ new float[] { 1f,   1f,   1f,   2f,   0.5f, 1f,   2f,   1f,   1f,   1f,   1f,   2f,   0.5f, 1f,   1f,   1f,   0.5f, 1f },
        /*PSY*/ new float[] { 1f,   1f,   1f,   1f,   1f,   1f,   2f,   2f,   1f,   1f,   0.5f, 1f,   1f,   1f,   1f,   0f,   0.5f, 1f },
        /*BUG*/ new float[] { 1f,   0.5f, 1f,   2f,   1f,   1f,   0.5f, 0.5f, 1f,   0.5f, 2f,   1f,   1f,   0.5f, 1f,   2f,   0.5f, 0.5f },
        /*ROC*/ new float[] { 1f,   2f,   1f,   1f,   1f,   2f,   0.5f, 1f,   0.5f, 2f,   1f,   2f,   1f,   1f,   1f,   1f,   0.5f, 1f },
        /*GHO*/ new float[] { 0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f },
        /*DRA*/ new float[] { 1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   2f,   1f,   0.5f, 0f },
        /*DAR*/ new float[] { 1f,   1f,   1f,   1f,   1f,   1f,   0.5f, 1f,   1f,   1f,   2f,   1f,   1f,   2f,   1f,   0.5f, 1f,   0.5f },
        /*STE*/ new float[] { 1f,   0.5f, 0.5f, 1f,   0.5f, 2f,   1f,   1f,   1f,   1f,   1f,   1f,   2f,   1f,   1f,   1f,   0.5f, 2f },
        /*FAI*/ new float[] { 1f,   0.5f, 1f,   1f,   1f,   1f,   2f,   0.5f, 1f,   1f,   1f,   1f,   1f,   1f,   2f,   2f,   0.5f, 1f }
    };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
        {
            return 1;
        }

        int row = (int)attackType - 1;
        int column = (int)defenseType - 1;

        return chart[row][column];
    }
}