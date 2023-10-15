UPDATE [Employees]
SET [Salary] = [SALARY] * 1.12
WHERE DepartmentID IN(1, 2, 4, 11)

SELECT [Salary]
FROM [Employees]