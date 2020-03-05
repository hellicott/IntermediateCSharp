# IntermediateCSharp

## Contents:

- [Delegates, Events and Lambda Expressions](#delegates-events-and-lambda-expressions)
- [Using .NET Framework APIs](#using-dotnet-framework-apis)
- [Generics in Depth](#generics-in-depth)
- [Additional C# Language Features](#additional-c-language-features)
- [LINQ to Objects](#linq-to-objects)
- [What's New in C# 8](#whats-new-in-c-8)
- [Monitoring and Debugging Applications](#monitoring-and-debugging-applications)
- [Unit Testing with xUnit](#unit-testing-with-xunit)
- [Test-driven Development](#test-driven-development)
- [Dependency Injection](#dependency-injection)
- [Test Doubles and Mocking](#test-doubles-and-mocking)

## Delegates, events and lambda expressions
**Contents:**
- [Memory](#memory)
- [Delegates](#delegates)
- [Lambda Expressions](#lamba-expressions)
- [Events](#events)

### Memory

#### Value Types

e.g. int, doubles, bools

The actual values get saved in the stack as they are fixed size.
The stack is evaluated at compile.

#### Reference Types

e.g. string and complex data types

The actual value gets saved in the heap and a reference to that value is saved in the stack. 
This is because we don't know how large the object might be.
The heap is evaluated at run time.

#### Memory Allocation

In java all class/own data types are reference types.
In C# we have a choice over this. We can decide which is more relevant.
For example a DateTime object simple stores a number. This is more efficiently stored in the stack 
rather than as a reference type.
In C# a `class` is saved as a reference type. A `struct` is stored as a value type.
Mostly a class is what you want, however sometimes, like with a DateTime, a struct is more efficient.
You can't use inheritance in a struct, however you can implement interfaces.

```
DateTime dt;
```
For a value type doing this will allocate the memory for that object 

```
DateTime dt = new DateTime()
```
This will both allocate the memory and call the constructor

```
Thing t;
```
For a reference type no memory is allocated when you do this. 

```
Thing 1 = new thing();
```
It's not until you use the `new` key word and 
call the constructor that memory is allocated

[[Back top of *Delegates, events and lambda expressions*]](#delegates-events-and-lambda-expressions)

### Delegates

Help prevent code duplication.

Give a function as a parameter, allowing you to use the same code to call whatever function you like.

The Delegate is like a type

```
public delegate double MathsFunction(double x);
```
You have to specify the input and return value and then any function which matches these 
values will be allowed to be passed in.

You can use it like this:
```
var x = doSomething(Math.Sin, a, b);
```
Notice we are not calling the `Math.Sin` function as we are not using brackets. Instead we are giving it as 
a delegate. This is allowed because the function has the same input and output values as defined in the delegate.

You can also have anonymous delegates. If you don't want to create a new Delegate type, you could achieve the same thing.

So this:
```
public void Something(Func<double, double>, int a, int b)
{
	// do stuff
}
```
Is the same as this:
```
public void Something(MathsFunction, int a, int b)
{
	// do stuff
}
```

You can do this with as many inputs as you like, but the last one will always be the return type. When there is no
return type, you must use an Action, instead of a Func.

Examples:

`delegate string A(int i)` = `Func<int, string>`

`delegate string B(int i, strings,  double d)` = `Func<int, string, double, string>`

`delegate string C()` = `Func<string>`

`delegate void D(int i)` = `Action<int>`

[[Back top of *Delegates, events and lambda expressions*]](#delegates-events-and-lambda-expressions)

### Lamba Expressions

Allow you to write an anonymous function at the point where you'd like to use it.

```
double Double (double x)
{
	return x * 2
}
```
The above function is good, but if we wanted to also have a `Triple` or `Quadruple` function we'd have a lot
of code duplication but hardly any changes. 

Instead we might want to use a lambda function:

```
var x = doSomething(x => x * 2, a, b);
```

This allows you to use any number as a multiplier, including a variable. 
It might be less readable, however, as you have to unnderstand the code rather than a nicely named function.

Another option would be to use a local function:
```
def doSomething()
{
	int mulitplier = 2;
	var x = doSomething(Mult, a, b)

	double Mult(double x)
	{
		return x * multiplier;
	}
}
```
This has the benefit of reabability, without code duplication and still being able to 
use variables as the local function is within the same scope.

#### Expression body members
You can have dynamically evaluated class properties. It is evaluated everytime you access the value.

For example:
```
DateTime date1 = datetime.now();
DateTime date2 => datetime.now();
```
Here `date1` will always be the same when you access it. However `date 2` will call the `datetime.now()` 
function everytime you access it.

#### Nullables
You can have nullable variables by defining it with `?` after the type.

There are also some shortcuts to work with values which may be null. They allow you to set a default return value
if something is null
```
var x = value ?? default;
```

You can also use it in a similar way to an if else statement:
```
var x = (doSomething) ? "yay" : "Oh no";
```

[[Back top of *Delegates, events and lambda expressions*]](#delegates-events-and-lambda-expressions)

### Events
Events are very similar to delegates they use the `event` key word.

Both allow you to multicast.
```
MathsFunction f = Math.Cos;
f += Math.Sin;
```
Using this function will carry out both the sin and cos, however it will only return the result of the last one
*not* chain them together. You would usually use multicasting with functions which have side effects but not return values
as you would only get the return of one value. 

If an event hasn't been subscribed to (nothing uses it in a multicast) then its value will be null. You should always
check for null before raising it to avoid `NullReferenceExceptions`. You can do this with an if statement:
```
if (Overdrawn != null)
{
    Overdrawn(this, new BankAccountEventArgs(balance, accountHolder));
}
```
```
Overdrawn?.Invoke(this, new BankAccnoutEventArgs(balance, accountHolder));
```

You can handle events in a few different ways:
```
BankAccount acc1 = new BankAccount("Peter");
acc1.Overdrawn += OnOverdrawn;

// Event handler method
private static void OnOverdrawn(object source, BankAccountEventArgs e)
{
    Console.WriteLine($"{e.AccountHolder} has overdrawn! Current Balance: {e.Balance}");
}
```
The one above uses a conventional event-handler method. Another option is to use a lambda function which
will infer the type:
```
BankAccount acc1 = new BankAccount("Peter");
acc1.ProtectionLimitExceeded += (source, e) => Console.WriteLine($"{e.AccountHolder} has gone over the protection limit!");             
```

In legacy code you might find an anonymous method (a delegate):
```
BankAccount acc1 = new BankAccount("Peter");
acc1.Overdrawn += delegate(object sender, BankAccountEventArgs e)
{
    Console.WriteLine($"{e.AccountHolder} has overdrawn! Current Balance: {e.Balance}");
}
```
[[Back top of *Delegates, events and lambda expressions*]](#delegates-events-and-lambda-expressions)

[[Back to Top]](#contents)

## Using dotNET Framework APIs
**Contents:**
- [Exception handling](#exception-handling)
- [Collections](#collections)
- [Regular Expressions](#regular-expressions)
- [NuGet packages](#nuget-packages)

### Exception handling
This is *not* for finding errors or fixing them, it is only for making the errors known when they happen - a communitcation
mechanism to send up so someone/something else can deal with it. In this way it is similar to an event.The main
difference between events and excpetions is that exceptions *must* be caught, where as events do no necessarily need to be 
handled.

All exceptions inherit from `System.Exception`

Handling Exceptions uses 3 key words. 
The `try` block contains code that might go wrong.
Any `catch` blocks specify the type of error you want to catch and contain how to handle this exception.
The `finally` block is optional but if it is there it is always called, whether there was an exception or not. It should be used to tidy up, for example
closing a file or disconnecting from a network.

Another option for the use case of closing a file is to use the `using` key word. Doing this means you don't need to 
close the file in a `finally` block, as instead it is automatically tidied up at the close of the `using` block

Exceptions are caught in the order the catch blocks are placed, so if you were to write this:
```
try
{
    // do stuff
}catch(Exception)
{
    //handle1
}catch(IOException)
{
    //handle2
}
```
The `handle2` code would never be called as any error would be caught by the first catch block

There is also exception filtering which can be used like this:
```
catch (exception) when (condition)
```

When creating your own exceptions you must inherit from `System.Exception` and implement the 3 methods. They use
the `innerException` which often contains more detailed information.

To throw an exception you use the `throw` key word

[[Back to top of *Using .NET Framework APIs*]](#using-dotnet-framework-apis)

### Collections

**Arrays**

Arrays are always reference types. When creating one you speficy the size which is used for memory allocation. The downside
here is that once you have set this you can't increase its size. It very unusual to know the size of a collection before
you want to use it so often other collections are used.

**Generic collections** 

e.g. `List<T>`, `HashSet<T>` where `T` is the type and `Dictionary<K,V> where `K` and `V` are the types of the keys and 
the values

These force all elements to be the same type (or you'll get a compilation error). Anoth benefit is that you can add as many
elements as you want to them at any time.

**Raw collections**

In legacy code you may find something like `ArrayList`. These are collections of objects, and therefore might be any type
in there and can involve a lot of casting.

[[Back to top of *Using .NET Framework APIs*]](#using-dotnet-framework-apis)

### Regular Expressions
Search for particular text items in strings using `Regex` class

|RE|Desc|Matches| Doesn't match|
|---|---|---|---|
|abc|matches the exact letters in the exact order|abc|def|
|.|matched any single character| r| rawr|
|[a-zA-Z]| matches any character in the set|p|9|
|[^a-zA-Z]|...
|\d|A digit|6|q
|\w|...

Useful Regex methods:
`Match` will find the first match in the string.
`Matches` finds all the non-overlapping matches in the string.
`Replace` searches for match and replaces it

[[Back to top of *Using .NET Framework APIs*]](#using-dotnet-framework-apis)

### NuGet packages
C# started with one standard library. It was large and was updated infrequently. To solve this problem they split up 
this large package into NuGet packages where you can search through microsoft and 3rd part packages and get the things
you actually need. 

Visual studio makes it easy to package and upload your own NuGet Packages, locally or publicly.

[[Back to top of *Using .NET Framework APIs*]](#using-dotnet-framework-apis)

[[Back to Top]](#contents)

## Generics in Depth
**Contents:**

- [Constraints](#constraints)
- [Inheritance](#inheritance)

[[Example Code]](Generics/Generics)

Compile errors are better than runtime errors. Compile errors are spotted by developers early on and won't get past 
a build or a pull request. Runtime errors can be harder to spot and therefore are possible to be spotted by customers.

Using raw types which involved using the type `object` rather than a specific type will not give compiler errors, 
but can have a problem at runtime. and as discussed, we don't want that. Generic types allow for type-safe classes 
which will compiler errors rather than holding back til runtime. It is usually better practice to use these.

You can also have generic methods. The syntax is similar, using `<T>` to specify the type.
```
static void GenericMethod<T>(T obj)
{
    // do stuff
}
```

### Constraints
This however, isn't often very useful as there's only 4 method that will work for all types.

Instead, we can specify which interfaces that T must implement to be able to use more methods. Like this:
```
static T Max<T> (T a, T b) where T : IComparable<T>
{
    if (a.CompareTo(b) < 0)
    {
        return a;
    }
    else
    {
        return b;
    }
}
```
Here we're ensuring all input types implement the IComparable interface and therefore we can safely use the
`CompareTo()` method. 

You can also use constraints to accept only value types (`T : struct`) or reference types (`T : class`). In order to 
return a null value as T you must ensure that T is a reference type as value types cannot be null. If you want to allow
both reference types and value types then instead of returning null you can return `default(T)` which will select a null
value for reference types and the default value (e.g. 0 for ints) for value types.

Another useful constraint is that the input type has a default constructor (`T : new()`). This would allow to write the 
following:
```
static T Method(T input) where T : new()
{
    T output = new T();
    return output;
}
```

[[Back to top of *Generics in Depth*]](#generics-in-depth)

### Inheritance
You can inherit from generic classes by specifying the types, therefore making the sub class no longer a generic type, 
you can keep the types generic, or you can do a mixture of the two.

[[Back to top of *Generics in Depth*]](#generics-in-depth)

[[Back to Top]](#contents)

## Additional C# Language Features

**Contents:** 
- [Tuples](#tuples)
- [Initialisation](#initialisation)
- [Anonymous Types](#anonymous-types)
- [Extension Methods](#extension-methods)

### Tuples
Useful to group together related data in a more adhoc way than a class. They are simple to create and to retrieve from, like
this:
```
var Person = ("Hannah", 21);

Console.WriteLine($"{Person.Item1} is {Person.Item2} years old");
```

You can also specify field names, making it a bit more readable:
```
var Person = (Name: "Hannah", Age: 21);

Console.WriteLine($"{Person.Name} is {Person.Age} years old");
```

As well as retrieving the values individually you can also unpack them all in one go, including just unpacking the ones
you care about.
```
(string name, int age) = Person;

//OR if you don't care about name:
(_, int age) = Person;
```

They are often used to easily return back mulitple values from a method, however sometimes in these situations defining a 
class would be much more readable.

**Out Parameters**
```
public void DoSomething(int[] refType, int valType)
{
    refType[0] = 1000000;
    valType = 36;
}
```

When parameters are passed into methods, reference types will pass a reference to the values, so if you change the values
within the method you will see these changes outside of it. However value types are passed as copies of that value, 
therefore changes made to the value will not be seen outside of the method.

When you want changes to value types to affect the original value you can use the keyword `ref`:
```
public void DoSomething(int[] refType, ref int valType)
{
    refType[0] = 1000000;
    valType = 36;
}

int i = 0;
int[] ints = {0, 1, 2, 3};
DoSomething(ints, ref i);
```
This, however, requires the input valueType to have already been initialised. If you want to use a value without it being
initialised, you can do this with the `out` keyword. You can even do this inline, so save declaring the variable beforehand:

```
public void DoSomething(int[] refType, out int valType)
{
    refType[0] = 1000000;
    valType = 36;
}

int i;
int[] ints = {0, 1, 2, 3};
DoSomething(ints, out i);

DoSomething(ints, out int j);
```

One fairly useful thing you can do with these is write a `Deconstruct` method for a class. This allows you to unpack a class
into individual variables.
```
public class Product
{
    public string Description { get; }
    public double UnitPrice { get; }

    public Product(string description, double unitPrice)
    {
        Description = description;
        UnitPrice = unitPrice;
    }
    public void Deconstruct(out string description, out double unitPrice)
    {
        description = this.Description;
        unitPrice = this.UnitPrice;
    }
}

Product productA = new Product("Carabiner", 12.99);
(string desc, double price) = Product;
```
Notice you don't need to call the `Deconstruct` method. when you write a line like this, C# will look for a 
`Deconstruct` method implementation and use that.

[[Back to top of *Additional C# Language Features*]](#additional-c-language-features)

### Initialisation
When a class has publicly settable properties you can initialise them all at the first instance like this:
```
public class Employee
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public int Grade { get; set; }
    public Employee() {}

    public Employee(string name, double salary)
    {
        Name = name;
        Salary = salary;
    }
}

Employee emp1 = new Employee { Name="Smith", Salary=20000, Grade=5 };
```

You can do similar in-line initialisation with many collections such as Dictionaries and Lists

[[Back to top of *Additional C# Language Features*]](#additional-c-language-features)

### Anonymous Types

Anonymous types have been largely overtaken by tuples. They are a way to define a nameless object, where the compiler 
infers types internally. They are declared like this:
```
var city2 = new
{
    Name = "Swansea",
    Country = "Wales",
    Longitude = 3.9,
    Latitude = 51.6
};
```
The main use of anonymous types is with LINQ which will be covered later in the course.

[[Back to top of *Additional C# Language Features*]](#additional-c-language-features)

### Extension Methods

[[Example Code]](ExtensionMethods)

Extension methods allow you to add functionality to objects which don't allow you to inherit from them. A lot of what 
they can do could also be acheived by static methods, however it is often more readable to use it a method as if it is
part of the original object. It has the added benefit that extension methods will show up in intellisense, making programming
easier.
```
public static class MyStringExtensionMethods
{
    public static int CountLetterA(this String str)
    {
        int count = 0;
        for (int i = 0; i < str.Length; i++)
        if (str[i] == 'A' || str[i] == 'a')
        count++;
        return count;
    }
}

string abc = "abc";
int numOfAs = abc.CountLetterA();
```

One thing extension methods allow you to do which you couldn't do without them is extend interfaces.

[[Back to top of *Additional C# Language Features*]](#additional-c-language-features)

[[Back to Top]](#contents)

## LINQ to Objects

[[Example Code]](LINQ/LINQ/LINQ)

**L**anguage **In**tegrated **Q**uery was created to replace using things like SQL in code. This was because when writing
these in C# they are written as strings which means you loose all syntax highlighting, intellisense and compile errors.

LINQ can use any object which implements `IEnumerable` as a data source. It uses the `from`, `where` `select` keywords
like SQL but they are lower case. An example query looks like this:

```
string[] cities = { "Boston", "New York", "Dallas", "St. Paul", "Las Vegas" };

var subset = from c in cities
             where c.Contains(" ")
             orderby c.Length
             ascending
             select c;
```

Once difference between SQL and LINQ is the order you put the statements in. This is to help intellisense suggestion as 
the `from` step tells intellisense the type. The `select` statement must be the last line of the query.

You can also change the values returned in the select statement, for example instead of selecting `c` you could select
`c.Population` and get back just that *column*. You can also return a string containing the info you need.
```
var summaries = from c in cities
                select String.Format("{0} in {1} has population of {2}.",
                                    c.Name, c.State, c.Population);
```

Or you can give back a subset of all columns using an anonymous type, called a projection.
```
var info = from c in cities
            where c.Population < 500000
            select new { c.Name, c.State };
```

You can also write queries in method syntax:
```
var subset = cities.Where(c => c.Population > 500000).Select(c.Name);
```

You can also perform set based operations on LINQ queries 
```
var diff = (from t in myTeams select t).Except(from t2 in yourTeams select t2);
```

[[Back to top of *LINQ to Objects*]](#linq-to-objects)

[[Back to Top]](#contents)

## What's New in C# 8

**Contents:**
- [Nullable Types](#nullable-types)
- [Pattern Matching](#pattern-matching)
- [Asynchronous Streams](#asynchronous-streams)
- [Even More New Stuff](#even-more-new-stuff)

### Nullable types
Value types are not nullable, but you can create nullable versions of them. You create them using this syntax:
```
int? nullableNum;
```
Nullable values have 2 new properties you can use on them `HasValue` and `Value`. One thing to be aware of is that if 
you wanted to use a nullable with a normal type, you could get null reference errors. For example:
```
int num2 = nullableNum;
```
would throw an error since `nullableNum` could be `null` and `num2` cannot contain null values. To safely do this there
are a couple of options:
```
// Give the value which is type int, not int?
int num2 = nullableNum.Value;

// Have a default value
int num2 = nullableNum ?? 0;
```

Reference types have always been nullable so there was no need to make them so. However in C# 8 they added the ability
to have non-nullable reference types. This can be very useful for objects which must contain other objects, for example
a `Car` must contain an `Engine`. With a nullable reference you could come to errors if you assume that the `Engine` is
not null.
```
class Car
{
    private Person _driver;
    private Engine _engine;
    
    public Car()
    {
        // do stuff
    }
}
```
In the above example both `_driver` and `_engine` and nullable reference types so if we were to try to access values in
`_engine` without checking if it was null we'd get an error. To make a non-nullable type we use the same syntax as with
nullable value types, so instead of specifying the non-nullable type we instead specify the nullable ones. Like this:
```
#nullable enable

class Car
{
    private Person? _driver;
    private Engine _engine;
    
    public Car()
    {
        // do stuff
    }
}
```
Notice the `#nullable enable` which is necessary to be able to use these non-nullable types, to keep compatibility with
older C# versions.

By using these types we should be able to avoid a lot of `NullReferenceException`s, so it is becoming increasingly 
advised to use these in new projects.

NOTE: In order to use these you must add `<Nullable>enable</nullable>` to your csproj to enable support.

[[Back to top of *What's New in C# 8*]](#whats-new-in-c-8)

### Pattern Matching
**Switch statements**

Added syntactic sugar for simple switches
```
string name;
switch(month)
{
    case 1:
        name = "January";
        break;
    case 2:
    ...
};

// can become:
string name = month switch
{
    1 => "January",
    2 => "February",
    ...
};
```

You can also now use switches on tuples:
```
switch (value1, value2, value3)
{
    case (aaa, bbb, ccc):
        // Do something
        break;
    case (ddd, eee, fff):
        // Do something else
        break;
    …
}
```
and use deconstructors:
```
string fullNum = telNum switch
{
    TelNum("UK", var num) => $"+44 {num}",
    TelNum("NO", var num) => $"+47 {num}",
    TelNum("SG", var num) => $"+65 {num}",
    _ => $"[{telNum.Country}] {telNum.Number}"
};
```
notice the `_` being used for default values.

[[Back to top of *What's New in C# 8*]](#whats-new-in-c-8)

### Asynchronous Streams

Before the C# 8 changes you might fetch data from a slow source like this:
```
//old implementation
public static async Task<IEnumerable<int>> FetchSlowData()
{
    List<int> items = new List<int>();
    for (int i = 1; i <= 10; i++)
    {
        items.Add(i);
    }
    return items;
}
```
This would return an `IEnumerable<int>` after all the data had been collected.
Then to use the data you could do something like this:
```
//old implementation
private static async Task Enumeration_SlowData()
{
    foreach (var item in await DataSrc.FetchSlowData())
    {
        Console.WriteLine(item);
    }
}
```
The downside to doing it in this way is that the code has to wait until all the data has been collected before is can 
loop over it.

C# 8 brings a solution to this - allowing the loop to iterate over the data as soon as each item becomes available. Here's
how you'd do it:
```
//new implementation
public static async IAsyncEnumerable<int> FetchSlowData()
{
    for (int i = 1; i <= 10; i++)
    {
        yield return i;
    }
}
```
Notice the new type `IAsyncEnumerable<T>` which is an async stream. This method yields one value as a time, as soon as
it's available. This allows you to loop over the data like this:
```
private static async Task Enumeration_SlowData()
{
    await foreach (var item in DataSrc.FetchSlowData())
    {
        Console.WriteLine(item);
    }
}
```
Notice the `await` keyword now applies to the entire loop, rather than just the method which retrieves the data.

Another way you could use the data is using the async enumerable API directly:
```
private static async Task Enumeration_SlowData()
{
    IAsyncEnumerator<int> asyncEnum = DataSrc.FetchSlowData().GetAsyncEnumerator();
    try
    {
        while (await asyncEnum.MoveNextAsync())
        {
            int item = asyncEnum.Current;
            Console.WriteLine(item);
        }
    }
    finally
    {
        await asyncEnum.DisposeAsync();
    }
}
```

[[Back to top of *What's New in C# 8*]](#whats-new-in-c-8)

### Even more new stuff

**Null-Coalescing Assignment Operator**

The `??` is called the null-coalescing operator. You can now use them for assignment too using `??=`. Here's and example:
```
List<int> numbers = MaybeGetList(); // This may be null
(numbers ??= new List<int>()).Add(42);
```

**Ranges**

You can, quite usefully, use ranges to access values in an array (not yet on all collections), similar to slicing in python.
```
// get values from a(inclusive) to b (exclusive)
arr[a..b];

// get values from beginning up to b (exclusive)
arr[..b];

// get values from a (inclusive) to end
arr[a..];

// get last n values
arr[^n];

// get all values except last n
arr[..^n];
```

**Disposing Objects**

Previously `using` statements dispose of the object at the end of the `using` block. However in C# 8 you no longer need 
a block for a `using` statement, instead the object will desposed at the end of the code block it is contained it. For 
example if it's contained in a method:

```
// before:
private static void UsingBlocks1(string inFilename)
{
    using (var reader = new StreamReader(inFilename))
    {
        var contents = reader.ReadToEnd();
        Console.WriteLine(contents);
    } <-- object disposed of here
}

//after:
private static void UsingDeclarations(string inFilename)
{
    using var reader = new StreamReader(inFilename);
    var contents = reader.ReadToEnd();
    Console.WriteLine(contents);
} <-- object disposed of here
```

this allows code to be a little tidier.

**Default Interface Implementation**

C# 8 allows you to write a concrete implementation for a method. This allows you to simply use the default implementation
when there is common code accross implementations, therefore avoiding code duplication. It still allows overriding for
when you need more specific functionality in an implementation.

It can be done like this:
```
interface IMyInterface
{
    void Method1(int i);
    void Method2(double d);
    void Method3(String s)
    {
        Console.WriteLine($"IParentInterface.Method3 received {s}");
    }
}
```

This is avoiding intruducing the horrors of multiple inheritance by still not allowing data to be held in an interface.

[[Back to top of *What's New in C# 8*]](#whats-new-in-c-8)

[[Back to Top]](#contents)

## Monitoring and Debugging Applications



[[Back to Top]](#contents)

## Unit Testing with xUnit

[[Back to Top]](#contents)

## Test-driven Development

[[Back to Top]](#contents)

## Dependency Injection

[[Back to Top]](#contents)

## Test Doubles and Mocking

[[Back to Top]](#contents)