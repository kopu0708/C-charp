using System;

public abstract class Skill //스킬을 구현한다고 가정하자
{
    public string SkillName { get; init; } //스킬이름은 바뀌지 않는다고 가정하자
    public abstract float CoolTime { get; set; } //쿨타임은 스킬마다 다르고, 시간에 따라 줄어들어야 하니 set도 추가

    public abstract void Use(); //추상 메서드 스킬마다 효과가 다르기 때문에

    //쿨타임 감소 로직 (모든 스킬이 공통으로 사용)
    public void DecreaseCoolTime(float amount)
    {
        CoolTime -= amount;
        if (CoolTime < 0)
        {
            CoolTime = 0;
        }
    }
}

public class Beam : Skill
{
    private float _coolTime; //실제 쿨타임 값을 저장할 필드

    public Beam()  //프로퍼티 초기화를 위한 생성자
    {
        SkillName = "광선";
        _coolTime = 10.5f;
    }

    public override float CoolTime //추상 프로퍼티도 구현 
    {
        get
        {
            return _coolTime;
        }
        set
        {
            _coolTime = value;
        }
    }

    //추상 메서드 구현 (override 키워드 필수)
    public override void Use()
    {
        Console.WriteLine($"{SkillName} 발사! 찌이이잉~ (쿨타임: {CoolTime}초)");
    }
}

public class Boom : Skill
{
    private float _coolTime; //실제 쿨타임 값을 저장할 필드

    public Boom()  //프로퍼티 초기화를 위한 생성자
    {
        SkillName = "폭탄";
        _coolTime = 1f;
    }

    public override float CoolTime //추상 프로퍼티도 구현 
    {
        get
        {
            return _coolTime;
        }
        set
        {
            _coolTime = value;
        }
    }


    //추상 메서드 구현 (override 키워드 필수)
    public override void Use()
    {
        Console.WriteLine($"{SkillName} 발사! 콰앙!~ (쿨타임: {CoolTime}초)");
    }
}
class Program
{
    static void Main()
    {
        Skill beam = new Beam();

        beam.Use();

        Skill boom = new Boom();

        boom.Use();   

        //실제 게임이라면 쿨타임을 체크하는 로직도 들어가고 해야겠지만 지금은 다형성을 확인하기 위한 용도로 쿨타임을 만들었다.

    }
}
