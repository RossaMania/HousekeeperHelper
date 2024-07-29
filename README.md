# Housekeeper Service Project

## Overview

The Housekeeper Service Project is designed to manage and automate the process of generating and sending statements to housekeepers. This project includes unit tests to ensure the reliability and maintainability of the codebase.

## Features

- Generate statements for housekeepers
- Send statements via email
- Handle email sending failures gracefully

## Technologies Used

- C#
- NUnit for unit testing
- Moq for mocking dependencies

## Project Structure

- **HousekeeperServiceProject**: Contains the main service logic.
- **HousekeeperServiceProject.Tests**: Contains unit tests for the service.

## Unit Testing

Unit testing is a crucial part of this project. It helps in catching bugs early, ensuring code quality, and making the codebase maintainable. The tests are written using NUnit and Moq.

### Benefits of Unit Testing

- Catch and fix bugs early
- Write better code with fewer bugs
- Produce software with better design
- Get rapid feedback on code changes
- Refactor code with confidence
- Act as documentation for the code

## How to Run Tests

1. Open the project in Visual Studio Code.
2. Ensure you have NUnit and Moq installed.
3. Run the tests using the built-in test runner or the following command in the terminal:

   ```sh
   dotnet test
   ```


## Example Test Cases

### HousekeeperServiceTests

- **SendStatementEmails_WhenCalled_GenerateStatements**: Verifies that statements are generated when the method is called.

### InvalidEmailTests

- **SendStatementEmails_InvalidEmail_ShouldNotGenerateStatements**: Ensures that statements are not generated for invalid email addresses.

### EmailStatementTests

- **SendStatementEmails_WhenCalled_EmailStatement**: Verifies that statements are emailed when the method is called.
- **SendStatementEmails_InvalidStatementFileName_ShouldNotEmailStatement**: Ensures that statements are not emailed for invalid statement file names.
- **SendStatementEmails_EmailSendingFails_ShouldShowMessageBox**: Verifies that a message box is shown when email sending fails.

## Course Information

This project is part of a course designed to teach unit testing from scratch. The course covers:

- Best practices for writing unit tests
- Tips and tricks for clean, maintainable tests
- Refactoring legacy code
- Dependency injection
- Using mocks to isolate code

### Course Benefits

- Learn unit testing from scratch
- Write clean, maintainable, and trustworthy tests
- Refactor legacy code towards testable code
- Understand and implement dependency injection
- Use mocks to isolate code from external dependencies
- Apply unit testing best practices
- Avoid common anti-patterns

### Prerequisites

- Minimum 3 months of programming experience in C#

## License

This project is licensed under the MIT License.

## Contact

For any questions or issues, please contact the course instructor or refer to the course materials.
```