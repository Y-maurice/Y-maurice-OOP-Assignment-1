## Problems i found in Order System Management app
- all data no matter it's related to what are all in one place 

- using separated arrays as data containers for things that should be grouped is very error prone (customer data is splited into multiple arrays and not grouped in one place), and also no modularization or separation of concerns

- no input validation (especially for date , it's a string so i can enter any string that is not a date)

- no validation for stock or price (if any was < 0)

- using double for money (can make precision issues)

- no encapsulation, data in arrays can be accessed from anywhere

- tight coupling between many things (Many operations depend on things they shouldn't)