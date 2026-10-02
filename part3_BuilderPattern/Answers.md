### 1 -  Why is a single 20-parameter constructor for this class a problem in practice? Think about call-site readability, the risk of passing two values in the wrong order (e.g. two decimal amounts, or two strings that are both addresses), and what happens the day someone adds one more optional property.

- It makes the code error-prone because the order of the parameters matters. If two parameters have the same type, such as two decimal amounts or two strings, accidentally swapping them will still compile but can cause a difficult-to-find bug.

- The constructor call is also hard to read because values like true, false, or null don't clearly indicate what property they belong to.

- It mixes required and optional values, making the constructor harder to understand and use.

- If another optional property is added, the constructor signature changes, so every call site may need to be updated, making the class harder to maintain.

### 2 - Is this purely a "constructor is too long" problem, or is there a deeper design issue with putting ~20 loosely related properties on a single class in the first place?

- It can be a "constructor is too long" problem, but I think that is only a symptom in some cases. The deeper issue is often the class design itself. If a class has around 20 loosely related properties, it may be managing different kinds of state or responsibilities that don't really belong together. In that case, the class might need to be split into multiple classes, each with a more focused responsibility.

### Why is the composed version better than the single big builder?
- Single responsibility: Each builder has one clear responsibility. AddressBuilder is responsible only for creating an Address, while OrderBuilder handles order and payment information. InvoiceBuilder is responsible for combining these parts into the final Invoice.

- Independent validation: AddressBuilder can make sure that an address has all of its required information without InvoiceBuilder needing to know anything about address validation rules.

- Reuse: The same AddressBuilder can be used to create both the billing and shipping addresses. Without this reuse, we would need separate BillingAddressBuilder and ShippingAddressBuilder classes containing the same code, which would lead to duplication.

- Readability at the call site: The composed version makes the different parts of the invoice clear when constructing it. Instead of having one large builder with methods for every property, we build the billing address, shipping address, and order separately and then combine them in the InvoiceBuilder. This makes it easier to understand what each part of the invoice represents.