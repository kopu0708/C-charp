# C# 
C# 공부한거 모아두기 교제는 '이것이 c#이다.' 를 사용하며 그외 gemini나 claude의 도움을 받기도 함 예제는 웬만해서는 책에 있는걸 참고하되 스스로 만들어 보는중이다.
## 스택과 힙 ## 
스택은 정적 메모리 할당에 사용되며 컴파일 시간에 크기가 정해진다.

함수 호출과 로컬 변수가 저장 함수가 종료되면 메모리가 자동으로 해제함

힙은 프로그램 실행 중에 동적으로 메모리 크기가 할당됨 

힙에 할당된 메모리는 누가 관리함? 내가 해야한다.

### 스택의 작동 원리 ###
가장 나중에 들어온 데이터가 가장 먼저 나간다. 이걸 LIFO(Last In First Out) 이라고 한다.

속도가 빠르다. 접근 방식도 단순하고 메모리 관리도 자동으로 해주고 편함 그런데 메모리 크기가 제한적이고. 이거 넘기면 스택 오버플로우 오류 생김

### 힙의 작동 원리 ###
프로그램 실행 중에 필요한 만큼만 할당하고 해체할 수 있다. 메모리 크기가 유동적이다.

다 사용한 메모리는 명시적으로 해체 하는게 좋다. 안하면 안됨?
안하면 프로그램 메모리 사용량이 누적되어 프로그램 성는이 저하된다. 메모리 누수라고도 함. 

근데 c#은 가비지 컬렉터가 주기적으로 더 이상 사용하지 않는 데이터를 수거해간다.

### 문자열 숫자로, 숫자를 문자열로 바꾸기 ###
c#에서는 문자열과 숫자 사이의 형 변환 방법을 Parse()라는 메소드로 한다. 이 메소드에 숫자로 변환할 문자열을 넘기면 숫자로 변환해준다.

int a = int.Parse("12345""); 이런 식이다.

숫자 형식의 데이터를 문자열로 바꾸는 방법도 있다. object로부터 물려받은 ToString() 메소드를 재정의(오버라이드) 했다.

int c = 12345;
string d = c.ToString();

이런 식이다.  연습해보자

### 상수와 열거 ### 
상수는 변수의 선언과 비슷하다 형식 앞에 const 키워드가 위치하고 상수가 가져야 하는 데이터를 반드시 대입해줘야 한다. 

const 자료형 상수명 = 값;

이렇게 선언한 상수는 변수와 똑같이 사용할 수 있다. 단 데이터를 변경할 수 없다.

Q.이게 왜 필요한가요?

A.프로그램에서 변경하면 안 되는 변수를 잊어버리고 실수로 변경하게 되면 프로그램이 많이 아프게 된다. 그런 실수를 없애기 위해서이다.

열거 형식은 여러 개의 상수를 정리해두는 것이다. 같은 범주에 속하는 상수를 여러 개 선언할 떄 유용하다. 

선언하는 방법은 

enum 열거_형식명 : 기반자료형 {상수1,상수2,상수3....} 

이런 식이다. 기반자료형은 정수 계열(byte, sbyte, short, ushort, int, uint, long, ulong, char)만 사용할 수 있으며 생략시 int를 기반자료형으로 사용한다.

enum DialogResult {Yes, no, CANCEL, CONFIRM, OK} 

이렇게 선언하면 차례대로 컴파일러가 0, 1, 2, 3 , 4 이런 값을 할당한다. 

열거 형식의 요소가 어떤 값을 가지는냐는 별 의미가 없고 각 요소가 중복되지 않는 값을 가지고 있는데 의미가 있기 때문이다.

DialogResult라는 열거 형식을 새롭게 만들었다. 그러므로 int나 float처럼 이 형식을 이용해 변수를 만들 수 있다. 

물론 담을 수 있는 데이터는 오직 위 형식의 요소들 뿐이다.

DialogResult result = DialogResult.YES; 

열거 형식의 변수를 선언하여 값을 대입하는 예이다.

### Nullable 형식 ### 
int 형식의 변수를 선언하면 4바이트의 메모리가 할당된다. c# 컴파일러는 이 메모리 공간이 비어있는 꼴을 못본다.

그런데 값이 비어있는 변수가 가끔 필요하다. 그럴 때 이 형식을 쓰면 된다. 선언 방법은

데이터 형식? 변수 이름;

이렇게 형식 뒤에 ?만 붙여주면 된다. 

모든 Nullable 형식은 HasValue와 Value 두가지 속성을 가지고 있다. 

HasValue 속성은 해당 변수가 값을 가지고 있는지 없는지를 나타내고, Value 속성은 변수에 담겨 있는 값을 나타낸다.

~~~
int? a = null;
Console.WriteLine(a.HasValue); // a는 null이므로 False 출력

a = 37;
Console.WriteLine(a.HasValue);// a는 37을 가지고 있으므로 true 출력
Console.WriteLine(a.Value); // 37을 출력
~~~
비어있는 변수에 Value 속성을 사용하면 CLR이 어쩌구 무슨 예외를 띄운다.

### Var ###
c#은 굉장히 깐깐하게 상수나 변수에 대해 형식 검사를 한다. var 키워들를 통해서 약한 형식의 검사를 하는 언어ㅢ 편리함도 지원한다.

var 사용하면 컴파일러가 알아서 해당 변수의 형식을 지정해준다. 그래서 반드시 선언과 동시에 초기화 시켜줘야 한다.

안그러면 컴파일러가 이게 무슨 형식인지 알 길이 없음

var a = 3 // a는 int 형식

참고로 var 키워드는 지역 변수로만 사용할 수 있다.

클래스의 필드를 선언할 때에는 반드시 명시적 형식을 선언해야 한다. 클래스의 필드는 선언과 같이 초기화하지 않는 경우가 대부분이라 컴파일러가 var로 선언하면 못알아 먹는다.

Q.object랑 뭐가 다른거임?

A.var는 컴파일러가 컴파일 할 때 타입을 추론해서 자료형을 정한다. 그리고 이건 바뀌지 않는다.

반대로 object는 런타임 내내 값이 바뀔 때 마다 유연하게 바뀐다. 

## 연산자 ##
덧셈, 뺼셈, 곱셈, 나눗셈 드의 산술 연산자랑 비트 연산자까지 많은데 안 쓸래야 안쓸 수 없으니깐 알아두자
### 산술 연산자 ###
+,-,*,/,% 이게 산술 연산자 차례대로 더하기, 빼기, 곱하기, 나누기(몫), 나누기(나머지) 임 수치 데이터 형식이면 다 사용가능함
result = a + b; <- 이렇게 쓰면 a 하고 b 더해서 result에 값 저장해줌 
### 증가 연산자와 감소 연산자 ###
++을 변수 옆에 붙이면 값을 1 증가 시켜주고 --를 붙이면 1 감소 시켜주는거 그런데 변수 앞에 붙는지 뒤에 붙는지가 중요함

앞에 붙으면 전위 증가/감소 연산자 라고 하고 뒤에 붙으면 후위 증가 연산자/감사 연산자 라고 함 

~~~
int a = 10;
Console.WriteLine(a++); // 11이 아닌, 10 출력 후 11로 증가시켜줌
Console.WriteLine(++a); // 12 출력 됨
~~~
이거 엄청 많이 씀 참고로 수치 데이터랑 열거 형식 데이터에 다 사용가능

### 관계 연산자 ###
한쪽이 큰지 작은지 등을 판단 해줌 <, >, <=, >=, ==, != 이렇게 있는데 차레로 오른쪽이 더큼, 왼쪽이 더큼, =붙은거는 같거나 크다, == 이거는 같다, !=는 다르다

이것도 많이 씀 

### 논리 연산자 ###
참과 거짓으로 이루어지는 진릿값이 피연산자인 연산

&&(and), ||(or), !(not)

### 삼항 연산자 ###
조건 연산자라고도 하는데 피연사자가 3개 인거 

~~~
int a = 30;
string result = a == 30 ? "삼십" : "삼십아님"; // result는 "삼십"
~~~
이렇게 쓰는데 조건식 ? 참일 때 값 : 거짓일 때 값 이런 식으로 구성 

### null 조건부 연산자 ### 
?. 가 하는 일은 객체의 멤버에 접근하기 전에 해당 객체가 null인지 검사하여 결과가 참이면 null을 반환하고 그렇지 않으면 .뒤에 지정된 멤버를 반환한다.

?[] 도 동일한 기능을 수행한다. 객체의 멤버 접근이 아닌 배열과 같은 컬렉션 객체의 첨자를 이용한 참조에 사용된다는 점이 다르다. 

~~~
public class User
{
    public string Name { get; set; }
}

// ...

User user = null;
string name;

// user가 null이므로 user.Name에 접근하지 않고, 'name' 변수에 즉시 null을 할당합니다.
string name = user?.Name; 

Console.WriteLine(name); // null이 출력됩니다. (예외 발생 안 함)

// 만약 user가 null이 아니라면
User user2 = new User { Name = "Alice" };
string name2 = user2?.Name;
Console.WriteLine(name2); // "Alice"가 출력됩니다.
~~~
이런 식으로 사용가능하다. ?. 는 if문을 쓸 곳에 사용해 코드 양을 줄일 수 있어 편리하다.

특히 그 진가는 객체가 중첩되어 있을 때 나타난다
~~~
public class Address
{
    public string Street { get; set; }
}

public class User
{
    public Address UserAddress { get; set; }
}

// ...

User user = new User(); // User 객체는 있지만, UserAddress 속성은 null입니다.

// 1. 기존 방식: 중첩된 if문 필요
string street;
if (user != null)
{
    if (user.UserAddress != null)
    {
        street = user.UserAddress.Street;
    }
}
// 'street'은 null입니다.

// 2. Null 조건부 연산자 사용
// user?.UserAddress 에서 UserAddress가 null이므로, 
// 그 뒤의 .Street에 접근하지 않고 즉시 null을 반환합니다.
string street2 = user?.UserAddress?.Street;

Console.WriteLine(street2); // null이 출력됩니다.
~~~
?[]는 배열이나 리스트가 null일때 유용하다
~~~
List<string> items = null;

// items가 null이므로 [0] 인덱스에 접근하지 않고 'firstItem'에 null을 할당합니다.
string firstItem = items?[0];

Console.WriteLine(firstItem); // null이 출력됩니다.

// ---
List<string> items2 = new List<string> { "Apple", "Banana" };
string firstItem2 = items2?[0];
Console.WriteLine(firstItem2); // "Apple"이 출력됩니다.
~~~
?? 이렇게 생긴 null 병합 연산자도 있는데 null검사를 간결하게 만들어주는 역할을 한다. 두 개의 피연산자를 받아들여 왼쪽 피연산자가 null인지 평가한다.

null이면 오른쪽 피연산자를 반환하고 아니면 왼쪽 피연산자를 그대로 반환한다. null 조건부 연산자와 함께 많이 사용한다.

~~~
User user = null;

// 1. user가 null -> user?.Name은 null이 됨
// 2. ?? (Null 병합) 연산자가 왼쪽이 null임을 확인
// 3. "Guest"를 'username' 변수에 할당
string username = user?.Name ?? "Guest";

Console.WriteLine(username); // "Guest"가 출력됩니다.

// 중첩 예제
User user2 = new User(); // UserAddress가 null
string street = user2?.UserAddress?.Street ?? "No Address";

Console.WriteLine(street); // "No Address"가 출력됩니다.
~~~
### 비트 연산자, 비트 논리 연산자 ###
비트 수준에서 데이터를 가공해야 하는 경우가 종종 생긴다. 이걸 위한 연산자 임

<< 왼쪽 시프트 연산자 첫 번째 피연사자의 비트를 두 번째 피연산자의 수만큼 왼쪽으로 이동 

>> 오른쪽 시프트 위와 동일함 오른쪽으로 이동시키는거

& 논리곱 수행  정수 계열 형식과 bool 형식에 대해 사용

| 논리합 수행 정수 계열 형식과 bool 형식에 대해 사용

^ 배타적 논리합 수행 (xor) 정수 계열 형식과 bool 형식에 대해 사용

~ 보수(not) 연산 수행 비트 반전 한다는 말임 

#### 연산자 우선순위 ####
이제 모든 연산자를 알아봤다 참고로 a = a + b 이런 식은 a += b 이렇게 줄여 쓸 수 있음 다른 것도 가능 해보셈

아무튼 연산자들도 일반적인 사칙연산 처럼 우선순위가 있다.
<img width="653" height="1010" alt="image" src="https://github.com/user-attachments/assets/bc658efa-4068-4a28-85fc-4d038d5ffa9f" />
알아만 두자 

