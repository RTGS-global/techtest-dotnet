# RTGS.global .NET Tech Test

This is the [RTGS.global](https://rtgs.global) tech test. We have implemented the following scenarios;

1. If a deposit of `1000` is made to `account A` the balance should be `1000`
2. If deposits of `1000` and `2000` are made to `account A` then the balance should be `3000`
3. If `1000` is transferred from `account A` to `account B` then `account A`'s balance should be `-1000` and `account B`'s balance should be `1000`

## Prerequisite
Download [.net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and an IDE that can run the project.

## Step 1

Review the code base...
 - What stands out? 
 - Can you identify any code smells?
 - How would you improve it?

## Step 2

Make the following enhancements:

 - Return appropriate error when trying to use an account that doesn't exist.
 - You should not be able to deposit a negative amount.
 - You should not be able to transfer money to the same account.
 - ...
