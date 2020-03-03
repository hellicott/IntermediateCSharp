# IntermediateCSharp

## Contents:

[Delegates, Events and Lambda Expressions](#delegates-events-and-lambda-expressions)
- [Memory](#memory)
- [Delegates](#delegates)
- [Lambda Expressions](#lamba-expressions)
  - [Expression Body Members](#expression-body-members)
  - [Nullables](#nullables)
- [Events](#events)


## Delegates, events and lambda expressions
### Memory

#### Value Types

e.g. int, doubles, bools

The actual values get saved in the stack as they are fixed size.
The stack is evaluated at compile.

#### Reference data

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
check for null before raising it to avoid errors

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


