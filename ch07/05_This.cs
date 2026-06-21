using System;

class Employee
{
    private string Name;
    private string Dept;

    public void SetName(string Name) //생성자로 만들면 this 안써도 되지 않나요? 그건 아니다 생성자에서도 매개변수와 필드의 이름이 같다면 필요하다
    {
        this.Name = Name; //여기서 this.Name은 필드 변수 Name은 그냥 매개 변수
    }

    public string GetName()
    {
        return Name; //리턴은 해당 필드를 반환하겠다는 말이다. 아까 DeepCopy에서도 썻었음 
    }

    public void SetDept(string Dept)
    {
        this.Dept = Dept;
    }

    public string GetDept()
    {
        return Dept;
    }

}

class MainApp
{
    static void Main()
    {
        Employee A = new Employee();
        A.SetName("이정철");
        A.SetDept("사장");
        Console.WriteLine($"{A.GetName()}, {A.GetDept()}");
    }
}
