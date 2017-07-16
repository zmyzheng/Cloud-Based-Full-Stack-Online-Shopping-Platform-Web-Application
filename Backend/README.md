## AUTO: Run script newservice.sh with your service name

## Commands to use
1. `dotnet restore`: Resolve all dependencies;
1. `dotnet build`: Build project;
1. `dotnet lambda deploy-function`: Deploy Lambda Function;
1. `dotnet test`: (For Test) build and run all tests;
1. `dotnet watch test`: (For Test) build and run all tests, also watch for any file changes and re-run tests.

## Instructions to add a new lambda function
**Say you want to create a new lambda function called TestService**

1. Create a directory under `Source/` called `TestService`;
1. Copy `aws-lambda-tools-defaults.json`, `Function.cs` and `project.json` from `Source/ExampleService` to `Source/TestService`;
1. Modify `aws-lambda-tools-defaults.json`:
    - Change `function-name` to `TestService`,
    - Change `function-handler` to match current function pattern, usually change `ExampleService` to `TestService` (2 occurances).
1. Modify `Function.cs`:
    - Change namespace to `TestService`.
1. Modify `project.json`, add dependencies;
1. Run `dotnet restore` under `Source/TestService` to see if there's any problem;
1. Start writing code ;) don't forget to add XML documentations.

## Instructions to add a new unit test
**Say you want to create a new unit test for TestService**

1. Create a directory under `Test/` called `TestService.Test`;
1. Copy `FunctionTest.cs` and `project.json` from `Test/ExampleService.Test` to `Test/TestService.Test`;
1. Modify `FunctionTest.cs`:
    - Change using statement to `TestService`,
    - Change namespace to `TestService.Test`.
1. Modify `project.json`:
    - Change project name for testing under `dependencies` from `ExampleService` to `TestService`,
    - Add other dependencies.
1. Run `dotnet restore` under `Test/TestService.Test` to see if there's any problem;
1. Start writing test code ;)
