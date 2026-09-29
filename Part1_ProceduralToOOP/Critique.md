## Problems i found in Order System Management app
- all data no matter it's related to what are all in one place
, the data and the logic, so there is nothing has a clear owner or responsibility which makes the program hard to read and maintain 

- the use of global variables makes the program missy,the state
can be changed from anywhere, which makes it hard to trace or debug the code

- using separated arrays as data containers for things that should be grouped is very error prone (customer data is splited into multiple arrays and not grouped in one place), and also no modularization or separation of concerns

- Relationships managed by array indexes, a link between customer and order for example can break silently

- Bussiness logic mixed with console output, functions like addCustomer or addProduct print error messages and return void, so the caller can't tell if the operation succeeded or not and rules cannot be tested without the console

- fixed size arrays with hard coded capacities, this wastes memory and the program can stop working if any max size reached

- no input validation (especially for date , it's a string so i can enter any string that is not a date)

- no validation for stock or price (if any was < 0)

- using double for money (can make precision issues)

- no encapsulation, data in arrays can be accessed from anywhere,
they can be read or changed by any function in the program.

- tight coupling between many things (Many operations depend on things they shouldn't)