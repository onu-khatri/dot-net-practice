// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Features.Exception_Filters;
using Features.String_Interpolation;

// StringInterpolation.PrintUserInfo(new("Anup", "Singh", "Khatri", "Mr", 0));
// StringInterpolation.TableView(new("Anup", "Singh", "Khatri", "Mr", 0));
// StringInterpolation.WithPatternMatch(new("Anup", "Singh", "Khatri", "Mr", 36));
// StringInterpolation.WithRawStringLiterals(new("Anup", "Singh", "Khatri", "Mr", 36));
// BenchmarkRunner.Run<StringFormatBenchmark>();


// ExceptionFilters.getSalaryToDonate(new ExceptionFilterUser("Anup Singh", Role.Admin, 2000, 0));
// ExceptionFilters.getSalaryToDonate(new ExceptionFilterUser("Anup Singh", Role.Manager,1500, 0));
// ExceptionFilters.getSalaryToDonate(new ExceptionFilterUser("Anup Singh", Role.User, 900, 5));

BenchmarkRunner.Run<ExceptionFilterBenchmark>();