# boggle

* Language - **C#**
* Runtime - **DotNet Core 3.1**
* Description - **Boggle board solver.**  **Contains both a single threaded and an async implementation.**

## Development & Debugging

* Install Visual Studio 2019 v16.6.5
* Ensure that DotNet Core 3.1 is installed
* Clone this repo
* Open the boggle.sln

## Commandline Testing & Execution

From a command prompt in the root of the repo

### Options

```bash
boggleApp 1.0.0
Copyright (C) 2020 boggleApp

  At least one option from group 'Threading' (s, singlethreaded, a, async) is required.
  -s, --singlethreaded          (Group: Threading) Enable or disable singlethreaded execution.  Default = true
  -a, --async                   (Group: Threading) Enable or disable async processing.  Default = false
  -l, --sidelength              (Default: 8) Board must be square, this is the length of a single side.  Default is 8, which will result in an 8x8 board.
  -f, --forcewritefoundwords    (Default: false) If more than 64 words are found, individual words are not written.  This forces all words written at the end of a run.  Default = false
  --help                        Display this help screen.
  --version                     Display version information.
```

### Run Tests

```bash
dotnet test
```

### Run console app - Single threaded

```bash
dotnet run --project src -c Release game --singlethreaded --sidelength=8
```

Valid --sidelength are values between 1 and 1024.

### Run console app - Async

```bash
dotnet run --project src -c Release game --async --sidelength=8
```

Valid --sidelength are values between 1 and 1024.
