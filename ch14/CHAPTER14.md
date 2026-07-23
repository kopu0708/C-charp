# 람다식 
항상 느끼는 건데 뭔가 이름이 동글동글한 느낌이라 귀여움 아무튼 왜 이름이 이럴까

영어로는 Lambda Expression 이라고 하는데 영어로 보니 그렇게 귀엽지 않은 이름이다. 

알론조 처치라는 수학자가 1936년에 발표한 람다 계산법에서 사용하는 식이다. 대충 간결하게 함수를 묘사하기 위해 고안해낸거다. 

람다 계산법은 크게 함수의 정의와 변수, 함수의 적용으로 이루어져 있는데 이 계산법에서는 모든 것이 함수로 이루어져 있다. 심지어 0,1,2 같은 정수도 말이다.

따라서 람다 계산법에서 어떤 값을 변수에 대입하고 싶으면 함수를 변수에 대입하며, 이걸 함수의 적용이라고 부른다.

이것을 존 매카시라는 사람이 프로그래밍 언어에 도입할 수 있겠다는 아이디어를 냈고 1950년대 말에 LISP라는 언어를 만들었다고 한다. 그리고 이게 C#, C++, 자바, 파이썬 등의 주류 언어에 도입 된거임

### 그래서 어떻게 하는건데? 
앞에 배웠던 익명 메소드가 기억하는가? 람다식은 이 익명 메소드를 만들기 위해 사용한다.

다만, 람다식으로 만드는 익명 메소드는 무명 함수라는 이름으로 부른다. 메소드는 입력(매개변수)과 출력(반환값)을 갖는다. 람다식도 마찬가지임

```C#
매개변수_목록 => 식
```
기본적인 람다식을 선언하는 형식은 위와 같다. => 연산자는 초면인데, 이 연산자는 '입력' 연산자이다. 

람다식에서는 =>를 중심으로 왼편에는 매개변수가, 오른편에는 식이 위치한다. 선언 예시를 보자 
```C#
delegate int Calculate(int a, int b); //익명 메소드를 만들려면 대리자가 필요하다.

// ...
static void Main()
{
  Calculate calc = (int a, int b) => a + b; // 두 개의 정수형을 매개변수 a, b로 받아 이 둘을 더해 반환하는 익명 메소드를 람다식으로 만들었다.
}
```
매우 간결하게 메소드가 표현되는 것을 알 수 있다. 그냥 옆에 이거 두개를 옆의 식으로 계산 해주세요~ 이거임 

심지어 여기서 더 간결하게 사용할 수가 있는데 형식 유추라는 기능을 제공하기 때문에 매개변수의 형식을 제거할 수 있다. 

```C#
Calculate calc = (a, b) => a + b;
```
이제 이게 뭔지 알았으니 적극 활용하자 (이상하게 잘 모르겠는 건 쓰기가 싫다. 자존심 상한다고 해야하나)

이제 예제 만들어 보자 [예제01](01_Lambda.cs)
### 문 형식의 람다식
문 형식? door말고 문장할때 문이다. 앞선 람다식은 말 그대로 식 형식을 하고 있었다. 식말고 문장으로 쓰면 더 읽기 쉬울수도 있지 않는가

그래서 있다. 문장을 사용할 수 있게 해줬다. 

=> 연산자의 오른편에 식 대신 중괄호로 둘러싸인 코드 블록이 위치한다.

```C#
(매개변수_목록) => {
                    문장1;
                    문장2;
                    문장3;
                  }
````
형식은 위와 같다. 식 형식으로는 반환 형식이 없는 무명 함수를 만들 수 없지만, 문 형식의 람다식을 이용하면 가능하다.

```c#
delegate void DoSomething();
//...
static void Main()
{
  DoSomething DoIt = () => // 당연하지만 매개변수가 없으면 그냥 ()
            {
                Console.WriteLine("뭔가");
                Console.WriteLine("출력");
                Console.WriteLine("하기");
            }; //{}로 둘러싸야함 문장형식의 람다식은
  DoIt();
}
```
예제 만들어보자 교재에 있는 걸로 [예제02](02_StatementLambda.cs)

### Func와 Action으로 더 간편하게 무명 함수 만들기 
익명 메소드와 무명 함수는 코드를 더 간결하게 만들어주는 요소들이다. 하지만 이들을 선언하기 위해 해야하는 작업이 있다.

대부분 단 하나의 익명 메소드나 무명 함수를 만들기 위해 매번 별개의 대리자를 선언해야 한다. 번거롭다면 번거로운 작업이다.(아니 이마저도 귀찮아? 맞긴해..)

아무튼 Func와 Action 대리자를 미리 만들어 뒀다. Func은 대리자는 결과를 반환하는 메소드를, Action 대리자는 결과를 반환하지 않는 메소드를 참조한다. 

#### Func 대리자 
결과를 반환하는 메소드를 참조하기 위해 만들어진 친구다. .NET에는 모두 17가지 버전의 Func 대리자가 준비되어 있다.
```C#
public delegate TResult Func<out TResult>()
public delegate TResult Func<in T, out TResult>(T arg)
public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2)
public delegate TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3)
...
public delegate TResult Func<in T1, in T2, in T3, ..., in T15, out TResult>(T1 arg1, T2 arg2, T3 arg3, ... T15 arg15)
public delegate TResult Func<in T1, in T2, in T3, ..., in T15, in T16, out TResult>(T1 arg1, T2 arg2, T3 arg3, ... T15 arg15, T16 arg16)
```
Func 대리자의 형식 매개변수 중 가장 마지막에 있는 것이 반환 형식이다. 형식 매개변수가 하나뿐인 Func는 그 하나가 반환 형식이다.

뭐 그 뒤로는 쭉쭉쭉 마지막 자료형이 반환형이다. 여기서 포인트는 Func을 사용하면 대리자 선언이 아예 사라진다는 점이다. 

예시를 보자 
```c#
Func<int> func1 = () => 10; // 입력 매개변수는 없으며, 무조건 10 반환
Console.WriteLine(func1()); // 10 출력

Func<int,int> func2 = (x) => x*2; //입력 매개변수는 int 형식 하나, 반환 형식도 int
Console.WriteLine(func2(3)); // 6을 출력
```
이런 식이다. 마지막을 제외하고 앞의 자료형의 데이터를 입력 받고 마지막의 자료형으로 반환 한다. 이렇게 이해하자 간단한 한번만 쓸 수식을 쓸때 좋을 듯 

[예제03](03_FuncTest.cs) 
#### Action 대리자
Func 대리자와 거의 똑같지만, 차이점이라면 Action 대리자는 반환 형식이 없다는 것이다. 마찬가지로 17개 버전이 선언되어 있다.

```c#
public delegate void Action()
public delegate void Action<in T>(T arg)
public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2)
public delegate void Action<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3)
...
public delegate  void Action<in T1, in T2, in T3, ..., in T16> (T1 arg1, T2 arg2, T3 arg3, ... , T16 arg16)
```
모두 입력 매개변수를 위해 선언되어 있다. Func와 달리 어떤 결과를 반환하는 것을 목적으로 하지 않고, 일련의 작업을 수행하는 것이 목적이기 때문이다.

매개변수가 아무것도 없는 Action의 사용은 반환하는 결과가 없다. 아래는 그 예시다.
```C#
Action act1 = () => Console.WriteLine("Action()");
act1();

// 다음은 매개변수가 하나뿐인 버전, Action<T>
int result = 0;
Action<int> act2 = (x) => result = x * x; // 람다식 밖에서 선언한 result에 x * x의 결과를 저장한다.

act2(3);
Console.WriteLine("result : {0}", result); // 9출력
```
별 차이점이 없다는 걸 알 수 있지만 이거는 결과는 필요없고 뭔가를 실행시키고 싶을때 사용한다. 출력이나 로그 남기기, 상태변경 같은거 말이다.

예제를 보자 [예제04](04_ActionTest.cs)

### 식 트리
식 트리라는 내용이 있는데 Linq를 이해하고 사용하는데 도움이 되는 내용이라고 한다. 지금 굳이 나에게 필요한 내용이 아닌 것 같아 일단 넘어가자(어차피 알고리즘 공부하면 나오는듯)

### 찐 막으로 식으로 이루어지는 멤버 
메소드를 비롯하여 속성(인덱서), 생성자, 종료자는 공통된 특징이 있다. 이들은 모두 클래스의 멤버로서 본문이 중괄호 {}로 만들어져 있다.

이러한 멤버의 본문을 식만으로 구현할 수 있는데. 이렇게 식으로 구현된 멤버를 식 본문 멤버라고 한다. 
```c#
멤버 => 식;
```
문법은 이렇다. 하나씩 어떻게 변하는지 보자 

메서드 
```c#
// 기존
public int Add(int a, int b)
{
    return a + b;
}

// 식 본문 멤버
public int Add(int a, int b) => a + b;
```
프로퍼티 
```c#
// 기존
public string Name
{
    get { return _name; }
}

// 식 본문 멤버
public string Name => _name;
```
인덱서
```c#
// 기존
public T this[int index]
{
    get { return _items[index]; }
}

// 식 본문 멤버
public T this[int index] => _items[index];
```
생성자
```c#
// 기존
public Monster(string name)
{
    Name = name;
}

// 식 본문 멤버
public Monster(string name) => Name = name;
```

예제를 만들어 보자 [예제05](05_ExpressionBoodiedMember.cs)
