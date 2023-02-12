namespace DesignEnum
{
    /// <summary> 
    /// ID : 1001
    /// 속성 ID
    /// </summary> 
    public enum AttributeId
    {
        Str = 0, //Str
        Vital = 1, //Vital
        Int = 2, //Int
        Luck = 3, //Luck
        Dex = 4, //Dex
    }
    /// <summary> 
    /// ID : 1002
    /// 시간 타입
    /// </summary> 
    public enum TimeType
    {
        Morning = 0, //Morning
        Noon = 1, //Noon
        Night = 2, //Night
        Midnight = 3, //Midnight
    }
    /// <summary> 
    /// ID : 1003
    /// 유닛 타입
    /// </summary> 
    public enum UnityType
    {
        Monster = 0, //Monster
        Character = 1, //Character
    }
    /// <summary> 
    /// ID : 1004
    /// 클래스 타입
    /// </summary> 
    public enum ClassType
    {
        Monk = 0, //Monk
    }
    /// <summary> 
    /// ID : 1005
    /// 버프 타입
    /// </summary> 
    public enum BuffType
    {
        AddATK = 0, //공격력 증가
        AddDEF = 1, //방어력 증가
        LowATK = 2, //공격력 감소
        LowDEF = 3, //방어력 감소
        Strun = 4, //스턴
        Dot = 5, //도트
    }
    /// <summary> 
    /// ID : 1005
    /// 스킬 타입
    /// </summary> 
    public enum SkillType
    {
        Normal = 0, //일반 공격
        Melee = 1, //근접 공격
        Range = 2, //범위 공격
    }
}
