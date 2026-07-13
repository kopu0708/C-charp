# 일반화 프로그래밍 

특수한 개념으로부터 공통된 개념을 찾아 묶느 것을 일반화(Generalization) 이라고 한다.  이러한 일반화를 이용하는 프로그래밍 기법이다.

이 일반화의 대상은 바로 데이터 형식이다. 예를 들어보자 정수형 1차원 배열을 복사하는 메소드를 작성했다고 치자 
```C#
void CopyArray(int[] source, int[] target)
{
  for(int i = 0; i < source.Length; i++)
      target[i] = source[i];
}
```
이 CopyArray() 메소드를 잘 사용하고 있었는데, 이번에는 문자열 배열을 복사하는 기능이 필요해졌다. 그래서 이걸 오버로딩 했다.

근데 이번에는 클래스 형식의 배열을 복사해야한다. 그래서 또 오버로딩하고.. 또 다른 클래스 형식을 반환해야해서 또 오버로딩하고.. 이렇게 31번 오버로딩했다.

근데 어차피 배열을 복사한다는 로직은 공통이지 않는가? 특수한 형식을 사용하는 코드를 일반화한다면 같은 메소드를 31가지 버전으로 만들지 않고도 모든 형식을 지원할 수 있을 것이다. 

그래서 이걸 어떻게 하냐? 일반화 메소드를 만드는 방법은 다음과 같다.

```C#
한정자 반환_형식 메소드_이름<형식_매개변수> (매개변수_목록)
{
    //.....
}
```
형식 매개변수가 뭔지 보자 
```C#
void CopyArray<T>(T[] source, T[] target)
{
  for(int i = 0; i < source.Length; i++)
      target[i] = source[i];
}
```
같은 메소드 지만 데이터 형식이 사용된 부분을 T기호로 치환했다. T는 형식을 뜻한다. T는 C#컴파일러가 지원하는 형식이 아니다. 따라서 T가 어떤 형식인지 알려줘야 한다.

형식 매개변수를 입력하는 방법은 다음과 같다. 메소드 이름 뒤에 <>를 쓰고 저기 사이에 T를 넣으면 형식매개변수가 된다. 그리고 메소드를 호출할 때 <>사이에 T대신 형식의 이름을 입력하면된다.

곧바로 예시로 넘어가서 보자 [예제01](01_GenericMethod.cs)

### 일반화 클래스 
일반화 클래스는 데이터 형식을 일반화한 클래스이다. 일반화 메서드는 메서드 하나만 일반화 시켰지만 클래스는 그 내부의 메소드 필드 모두 일반화 시키는 거다.

이로 인해 같은 성질의 클래스 객체를 자료형 마다 여러개 만들 필요가 없어졌다. 선언 방법은 일반적인 클래스 선언과 같지만 뒤에 <형식_매개변수>가 붙는 다는 점만 다르다. 
```C#
class 클래스_이름 <형식_매개변수>
{
  private T[] array;
  // ...
  public T 메소드_이름() {//...}
}
```
사용하는 법은 다음과 같다 
```C#
Array_Generic<int> intArr = new Array_Generic<int>();
```
예제를 보자 [예제02](02_GenericClass.cs)

### 형식 매개변수 제약시키기 
T는 모든 데이터 형식을 대신할 수 있었다. 종종 특정 조건을 갖춘 형식에만 대응하는 형식 매개변수가 필요할 때도 있다. 예를 숫자의 크기를 비교해주는 클래스를 만들었다 하자 

```C#
class MyList<T>
{
    public T Max(T a, T b)
    {
        return a > b ? a : b; // 오류가 난다 T가 >비교를 지원하는지 모르기 때문에 
    }
}
```
다음과 같이 말이다. float,double,int와 같은 형식들은 문제가 없지만 그 외의 형들은 지원하지 않기 때문이다. 이때 필요한게 제약조건이다. 그 방법은 where 키워드 이다. 
```C#
class 클래스_이름<T> where T : 제약조건
```
다음은 제약조건 목록이다. 
| 제약 | 의미 |
|------|------|
| `where T : struct` | T는 값 형식만 |
| `where T : class` | T는 참조 형식만 |
| `where T : new()` | T는 매개변수 없는 생성자를 가져야 함 |
| `where T : 클래스이름` | T는 해당 클래스를 상속받아야 함 |
| `where T : 인터페이스이름` | T는 해당 인터페이스를 구현해야 함 |
| `where T : U`| T는 또 다른 형식 매개변수 U로부터 상속받은 클래스여야 함|

그럼 하나 씩 예시를 보자 
```C#
// T는 반드시 IComparable을 구현한 형식만 가능
class MyList<T> where T : IComparable
{
    public T Max(T a, T b)
    {
        if (a.CompareTo(b) > 0)  // IComparable이 보장되니까 사용 가능
            return a;
        return b;
    }
}

// int, string 등 IComparable을 구현한 형식은 OK
MyList<int>    list1 = new MyList<int>();
MyList<string> list2 = new MyList<string>();
```
struct,class와 클래스 이름 인터페이스 이름은 이 사례와 거의 같기 때문에 패스하자 그럼 남은 것은 new와 U이다. 

new()의 예를 보겠다.

```C#
public static T CreateInstance<T>() where T : new()
{
  return new T();
}
```
이 코드의 메소드는 기본 생성자를 가진 어떤 클래스의 객체라도 생성해준다. 기본 생성자가 없는 클래스를 형식 매개변수에 넘기면 당연히 컴파일 에러가 난다.

다음은 상위 코드에서 사용되던 형식 매개변수 U로부터 상속받는 형식으로 제약 조건을 주는 예이다.

```C#
class BaseArray<U> where U : Base
{
    public U[] Array { get; set; }
    public BaseArray(int size)
    {
        Array = new U[size];
    }

    public void CopyArray<T>(T[] Source) where T : U
    {
        Source.CopyTo(Array, 0);
    }
}
```
BaseArray<U>는 Base를 상속받은 형식만 담을 수 있는 배열 클래스다. CopyArray<T>는 거기에 더해 T가 U를 상속받은 형식이어야만 복사할 수 있도록 제약을 건다. 즉 U의 자식 형식만 복사 가능하다.

참고로 이러한 제약 조건은 한번에 여러개 주는 것도 가능하다.
```c#
class MyClass<T> where T : class, IComparable, new()
// T는 참조 형식이면서, IComparable을 구현하고, 기본 생성자도 있어야 함
```
한번에 이해가 가지는 않지만 예제를 만들어보고 연습해보면서 익혀보자.  마찬가지로 게임개발의 맥락에서 생각해보면 아이템에는 여러 종류가 있다.

이미 우리는 클래스와 인터페이스 프로퍼티 등으로 다형성을 구현할 수 있지만 특정 데이터 형식 일때만 로직을 실행시키고 싶을 때가 있을 수도 있다. 이를 예시로 예제를 만들어 보자

[예제03](03_Constraint_type.cs)

인벤토리를 구현했을때를 예를 들었다. 

예제03 — Inventory<T> 에서 사용한 제약조건 정리

- where T : Item       → 아이템만 인벤토리에 담을 수 있음
- where W : Weapon     → 무기만 강화 가능
- where U : Item, new() → 기본 생성자가 있는 아이템만 생성 가능

### 일반화 컬렉션 
앞서 우리는 이미 컬렉션을 학습했었다. ch10에서 제네릭을 짧게 말하고 넘어갔었는데 이제 그 이야기가 나온다. 앞서 이야기 했듯이 앞서 등장한 컬랙션들은 데이터 형과 상관없이 모두 넣을 수 있었다.

object 형식을 기반으로 하기 때문에 가능했던 것이였다. 하지만 그로인해 박싱과 언박싱이 발생해 성능저하가 따라왔었다.

이를 해결하기 위해 제네릭이 도입되면서 각자 업그레이드 버전이 생겼었다고 설명했었다. 

List<T>, Queue<T>, Stack<T>, Dictionary<Tkey, TValue> 모두 각각 10장에서 봤던 애들의 일반화 버전이다. 기능과 동작 방식은 동일하기에 예제는 그냥 한번에 4개 보고 넘어가자

[ListEx](04_ListEx.cs), [QueueEx](05_QueueEx.cs), [StackEx](06_StackEx.cs), [DictionaryEx](07_DictionaryEx.cs)

또한 앞서 예제에서 만든 Inventory<T>가 List<T>와 같은 원리로 작동한다. 만약 실무였다면 그냥 List<T> 쓰면 될 듯 위 컬렉션의 메서드들은 인터넷에서 찾아 공부하자

### foreach는 그냥 쓸 수 없다.
앞서 foreach를 사용할 수 있는 클래스를 만드는 법을 학습헀었다. IEnumerable 인터페이스를 상속해서 클래스를 만드는 방법말이다.

일반화 클래스도 위와 같은 방식으로 인터페이스를 상속하면 일단은 foreach를 통해 순회가 가능하지만 요소를 순회할 때마다 형식 변환을 수행하는 오버헤드가 발생한다.

그럼 어캐하나요?

놀랍게도 IEnumerable에도 일반화 버전인 IEnumerable<T> 인터페이스가 존재한다. 이 인터페이스를 상속하면 형식 변환으로 인한 성능 저하가 없으면서도 foreach 순회가 가능하다.

그럼 이것도 인터페이스니깐 구현해야하는 메소드들이 있겠네요? 확실히 사실이다 한번 보자 
| 메소드 | 설명 |
|------|------|
| `IEnumerator GetEnumerator()` | IEnumerator 형식의 객체를 반환(IEnumerable로부터 상속받은 메소드 |
| `IEnumerator<T> GetEnumerator()` | IEnumerator<T> 형식의 객체를 반환 |
다음과 같이 같은 메소드를 두 개나 갖고 있다. 이름은 같지만 반환 형식이 다르다. IEnumerator를 반환하는 버전은 일반화된 인터페이스가 IEnumerator 인터페이스로부터 상속받은 것이다.

IEnumerator<T>를 반환하는 버전은 새로 선언된 메소드이다. 아무튼 우린 이 둘을 모두 구현해야한다. 왜요? 상속받았으니깐 

IEnumerable<T>가 IEnumerable을 상속하고 있기 때문이다. 부모 인터페이스의 메서드까지 모두 구현해야 하는 인터페이스 규칙 때문에 두 버전을 다 구현해야 한다.

| 메소드 또는 프로퍼티 | 설명 |
|-------|-------|
| `boolean MoveNext()` | 다음 요소로 이동한다. 컬렉션의 끝을 지난 경우에는 false, 이동이 성공한 경우에는 true를 반환한다.|
| `void Reset()` | 컬렉션의 첫 번째 위치의 '앞'으로 이동한다. 첫 번째 위치가 0번일 때, Reset()을 호출하면 -1번으로 이동한다. 첫 번째 위치로의 이동은 MoveNext()호출 다음이다.|
| `Object Current{get;}` | 현재 요소를 반환한다.(IEnumerator로부터 상속받은 프로퍼티)|
| `T Current{get;}` | 컬렉션의 현재 요소를 반환한다.|

이렇게 IEnumerator<T>의 메소드와 프로퍼티를 보았다. Current프로퍼티가 두 가지 버전인 것을 확인 할 수 있다. 역시 두가지 모두 다 구현해야한다. 

IEnumerator<T> 나 IEnumerable<T>는 형식 매개변수를 제외하면 원본과 차이점이 거의 없다. 아까 인벤토리 만든 걸 수정하는 걸로 예제를 보자 





