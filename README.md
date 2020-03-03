# IntermediateCSharp

## Memory

### Value Types

e.g. int, doubles, bools

The actual values get saved in the stack as they are fixed size.
The stack is evaluated at compile.

### Reference data

e.g. string and complex data types

The actual value gets saved in the heap and a reference to that value is saved in the stack. 
This is because we don't know how large the object might be.
The heap is evaluated at run time.

### Memory Allocation

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

## Delegates

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

### Lamba Functions

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

