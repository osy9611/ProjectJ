namespace DesignEnum
{
    /// <summary> 
    /// ID : 1001
    /// ?�성 ID
    /// </summary> 
    public enum AttributeId
    {
        Atk = 0, //Atk
        Def = 1, //Def
    }
    /// <summary> 
    /// ID : 1002
    /// ?�간 ?�??
    /// </summary> 
    public enum TimeType
    {
        All = 0, //All
        Morning = 1, //Morning
        Noon = 2, //Noon
        Night = 3, //Night
        Midnight = 4, //Midnight
    }
    /// <summary> 
    /// ID : 1003
    /// ?�닛 ?�??
    /// </summary> 
    public enum UnitType
    {
        Monster = 0, //Monster
        Character = 1, //Character
    }
    /// <summary> 
    /// ID : 1004
    /// ?�래???�??
    /// </summary> 
    public enum ClassType
    {
        Monk = 0, //Monk
    }
    /// <summary> 
    /// ID : 1005
    /// 버프 ?�??
    /// </summary> 
    public enum BuffType
    {
        AddATK = 0, //공격??증�?
        AddDEF = 1, //방어??증�?
        LowATK = 2, //공격??감소
        LowDEF = 3, //방어??감소
        Strun = 4, //?�턴
        Dot = 5, //?�트
    }
    /// <summary> 
    /// ID : 1005
    /// ?�킬 ?�??
    /// </summary> 
    public enum SkillType
    {
        Normal = 0, //?�반 공격
        Melee = 1, //근접 공격
        Range = 2, //범위 공격
    }
    /// <summary> 
    /// ID : 1007
    /// ?�별
    /// </summary> 
    public enum Gender
    {
        Male = 0, //?�자
        Female = 1, //?�자
    }
    /// <summary> 
    /// ID : 1008
    /// ?�킬 ID
    /// </summary> 
    public enum SkillID
    {
        NormalAttack1 = 101, //?�반 공격 1
        NormalAttack2 = 102, //?�반 공격 2
        NormalAttack3 = 103, //?�반 공격 3
        Skill1 = 201, //?�킬 공격 1
        Skill2 = 202, //?�킬 공격 2
        Skill3 = 203, //?�킬 공격 3
        Skill4 = 204, //?�킬 공격 4
    }
    /// <summary> 
    /// ID : 1009
    /// ?�킬 공격 ?�??
    /// </summary> 
    public enum SkillAttackType
    {
        Circle = 0, //?�형
        Straight = 1, //직선
    }
    /// <summary> 
    /// ID : 1010
    /// 몬스???�??
    /// </summary> 
    public enum MonsterType
    {
        FieldNormal = 0, //?�드 ?�반
        DungeonNormal = 1, //?�전 ?�반
        FieldBoss = 2, //?�드 보스
        DungeonBoss = 3, //?�전 보스
    }
    /// <summary> 
    /// ID : 1011
    /// ?�드 ?�??
    /// </summary> 
    public enum FieldType
    {
        Field = 0, //?�드
        Dungeon = 1, //?�전
    }
}
