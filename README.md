Assumptions and decisions:
1. I applied a clean architecture approach.
![image](https://user-images.githubusercontent.com/32266142/182232309-33b0df39-59a8-4e7a-82f6-ea8261fabafb.png)
2. I partially applied the DDD approach. I refined the Domain core from all dependencies. And created domain services to find top N sold products.
3. Web Application and Console application use the same use case from the Application layer. So we don't duplicate any logic.
4. Requirements say that I need not only to return the top 5 sold products but to change product stock for the same use case.
This part violates the Single Responsibility Principle and Command Query Separation Principle. I don't like it but I followed the description of the task.
6. All my code is only for demonstration purposes. We can discuss all the missing elements in an interview.

As soon as it is test assessment and I have restricted time:
1. I did not implement any error handling logic
2. I created only one unit test
3. It is not a production quality code
4. I did not use MediatR, AutoMapper, etc. Time to set all tools up I put in implementing bare minimum.
5. I did not use Polly and retry policies in my solution. We can discuss how can we use Polly for the integration with ChannelEngine API.
6. Please read all my comment.
7. It should be a global error handler (filter or middleware) for ASP.NET Core App and Console App. We can discuss diffrent ways how to do it.
8. Cancelation Tokes added for the demonstration purposes. Since we can use our application layer for different types of applications.

Technical:
I used .NET6 framework.

Requirements:
Application Entry points:
● A .NET console application which can execute the business logic listed below.
Write the results of the logic below to the console output.
● An ASP.NET application, which can execute the business logic listed below.
Implement this using a controller which displays an HTML table with the results.
Business logic:
Create the following methods in a shared library:
● Fetch all orders with status IN_PROGRESS from the API
● From these orders, compile a list of the top 5 products sold (product name, GTIN
and total quantity), order these by the total quantity sold in descending order
● Pick one of the products from these orders and use the API to set the stock of
this product to 25.
Testing:
● A unit test testing the expected outcome of the “top 5” functionality based on
dummy input.
