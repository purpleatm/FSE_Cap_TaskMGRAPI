.\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe -register:user "-filter:+[*]*" "-target:.\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe" "-targetargs:.\UnitTest\TaskManager.DataAccess.Tests\bin\Debug\TaskManager.DataAccess.Tests.dll"

.\packages\ReportGenerator.2.1.8.0\tools\ReportGenerator.exe "-reports:results.xml" "-targetdir:.\coverage"

pause